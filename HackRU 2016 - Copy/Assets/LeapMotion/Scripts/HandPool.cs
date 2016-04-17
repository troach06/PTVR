﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using Leap;

namespace Leap.Unity {
  /** 
   * HandPool holds a pool of IHandModels and makes HandRepresentations 
   * when given a Leap Hand and a model type of graphics or physics.
   * When a HandRepresentation is created, an IHandModel is removed from the pool.
   * When a HandRepresentation is finished, its IHandModel is returned to the pool.
   */
  public class HandPool :
    HandFactory {

    [SerializeField]
    private List<ModelPair> ModelCollection;
    [SerializeField]
    private List<ModelGroup> ModelPool;
    public bool EnforceHandedness = false;
    
    [System.Serializable]
    public class ModelPair {
      public IHandModel LeftModel;
      public IHandModel RightModel;
      public ModelPair(IHandModel leftModel, IHandModel rightModel) {
        this.LeftModel = leftModel;
        this.RightModel = rightModel;
      }
    }
    [System.Serializable]
    public class ModelGroup {
      public List<IHandModel> modelList;
      public ModelGroup(List<IHandModel> modelList) {
        this.modelList = modelList;
      }
    }

    private Dictionary<IHandModel, ModelGroup> modelGroupMapping = new  Dictionary<IHandModel, ModelGroup>();

    /** Popuates the ModelPool with the contents of the ModelCollection */
    void Start() {
      ModelPool = new List<ModelGroup>();
      foreach (ModelPair pair in ModelCollection) {
        ModelGroup newModelGroup = new ModelGroup(new List<IHandModel>());
        newModelGroup.modelList.Add(pair.LeftModel);
        modelGroupMapping.Add(pair.LeftModel, newModelGroup);
        newModelGroup.modelList.Add(pair.RightModel);
        modelGroupMapping.Add(pair.RightModel, newModelGroup);
        ModelPool.Add(newModelGroup);
      }
    }
 
    /**
     * MakeHandRepresentation receives a Hand and combines that with an IHandModel to create a HandRepresentation
     * @param hand The Leap Hand data to be drive an IHandModel
     * @param modelType Filters for a type of hand model, for example, physics or graphics hands.
     */
    public override HandRepresentation MakeHandRepresentation(Hand hand, ModelType modelType) {
      HandRepresentation handRep = null;
      List<IHandModel> models = new List<IHandModel>();
      foreach (ModelGroup group in ModelPool) {
        for (int i = 0; i < group.modelList.Count; i++) {
          IHandModel model = group.modelList[i];
          bool isCorrectHandedness;
          Chirality handChirality = hand.IsRight ? Chirality.Right : Chirality.Left;
          isCorrectHandedness = model.Handedness == handChirality;
          if (!EnforceHandedness || model.Handedness == Chirality.Either) {
            isCorrectHandedness = true;
          }
          bool isCorrectModelType;
          isCorrectModelType = model.HandModelType == modelType;
          if (isCorrectModelType && isCorrectHandedness) {
            group.modelList.RemoveAt(i);
            //--i;
            models.Add(model);
            break;
          }
        }
      }
      handRep = new HandProxy(this, models, hand);
      return handRep;
    }

    public void ReturnToPool(IHandModel model){
      ModelGroup modelGroup = modelGroupMapping[model];
      modelGroup.modelList.Add(model);
    }

#if UNITY_EDITOR
    /**In the Unity Editor, Validate that the IHandModel is an instance of a prefab from the scene vs. a prefab from the project. */
    void OnValidate() {
      for (int i = 0; i < ModelCollection.Count; i++) {
        if (ModelCollection[i] != null) {
          if (ModelCollection[i].LeftModel) {
            ValidateIHandModelPrefab(ModelCollection[i].LeftModel);
          }
          if (ModelCollection[i].RightModel) {
            ValidateIHandModelPrefab(ModelCollection[i].RightModel);
          }
        }
      }
    }
    void ValidateIHandModelPrefab(IHandModel iHandModel) {
      if (PrefabUtility.GetPrefabType(iHandModel) == PrefabType.Prefab) {
        EditorUtility.DisplayDialog("Warning", "This slot needs to have an instance of a prefab from your scene. Make your hand prefab a child of the LeapHanadContrller in your scene,  then drag here", "OK");
      }
    }
#endif
  }
}
