// Copyright 2016 Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// Handler for UI buttons on the scene.  Also performs some
// necessary setup (initializing the firebase app, etc) on
// startup.
public class UIHandlerDB : MonoBehaviour {

  ArrayList moleculeList = new ArrayList();
  Vector2 scrollPosition = Vector2.zero;
  private Vector2 controlsScrollViewVector = Vector2.zero;

  public GUISkin fb_GUISkin;
  public GameObject userData;

  private Vector2 scrollViewVector = Vector2.zero;
  protected bool UIEnabled = true;

  const int kMaxLogSize = 16382;
  DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;

  // When the app starts, check to make sure that we have
  // the required dependencies to use Firebase, and if not,
  // add them if possible.
  protected virtual void Start() {    

    FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
      dependencyStatus = task.Result;
      if (dependencyStatus == DependencyStatus.Available) {
        InitializeFirebase();
      } else {
        Debug.LogError(
          "Could not resolve all Firebase dependencies: " + dependencyStatus);
      }
    }); 
  }

  // Initialize the Firebase database:
  protected virtual void InitializeFirebase() {
    FirebaseApp app = FirebaseApp.DefaultInstance;
    app.SetEditorDatabaseUrl("https://logiachem.firebaseio.com/");
    if (app.Options.DatabaseUrl != null) app.SetEditorDatabaseUrl(app.Options.DatabaseUrl);
    StartListener();
  }

  protected void StartListener() {
    FirebaseDatabase.DefaultInstance.GetReference("users/top").GetValueAsync().ContinueWith(task => {
        if (task.IsFaulted) {
            // Handle the error...
        }
        else if (task.IsCompleted) {
            DataSnapshot snapshot = task.Result;
            // Do something with snapshot...
        }
      });       
  }

  // Exit if escape (or back, on mobile) is pressed.
  protected virtual void Update() {
    if (Input.GetKeyDown(KeyCode.Escape)) {
      Application.Quit();
    }
  }

  // Output text to the debug log text field, as well as the console.
  

  // Render the log output in a scroll view.


  // Render the buttons and other controls.
  void GUIDisplayControls() {
    if (UIEnabled) {
      controlsScrollViewVector =
      GUILayout.BeginScrollView(controlsScrollViewVector);
      GUILayout.BeginVertical();

      GUILayout.Space(20);  

      GUILayout.EndVertical();
      GUILayout.EndScrollView();
    }
  }



  // Render the GUI:
  void OnGUI() {
    GUI.skin = fb_GUISkin;
    if (dependencyStatus != DependencyStatus.Available) {
      GUILayout.Label("One or more Firebase dependencies are not present.");
      GUILayout.Label("Current dependency status: " + dependencyStatus.ToString());
      return;
    }
    Rect logArea, controlArea, moleculeListArea;

    if (Screen.width < Screen.height) {
      // Portrait mode
      controlArea = new Rect(0.0f, 0.0f, Screen.width, Screen.height * 0.5f);
      moleculeListArea = new Rect(0, Screen.height * 0.5f, Screen.width * 0.5f, Screen.height * 0.5f);
      logArea = new Rect(Screen.width * 0.5f, Screen.height * 0.5f, Screen.width * 0.5f, Screen.height * 0.5f);
    } else {
      // Landscape mode
      controlArea = new Rect(0.0f, 0.0f, Screen.width * 0.5f, Screen.height);
      moleculeListArea = new Rect(Screen.width * 0.5f, 0, Screen.width * 0.5f, Screen.height * 0.5f);
      logArea = new Rect(Screen.width * 0.5f, Screen.height * 0.5f, Screen.width * 0.5f, Screen.height * 0.5f);
    }

    GUILayout.BeginArea(moleculeListArea);
    GUILayout.EndArea();

    GUILayout.BeginArea(logArea);
    GUILayout.EndArea();

    GUILayout.BeginArea(controlArea);
    GUIDisplayControls();
    GUILayout.EndArea();
  }
}
