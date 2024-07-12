using Firebase.Auth;
using Firebase.Database;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class FireBaseManager : MonoBehaviour
{
    private FirebaseAuth auth;


    public InputField ipEmail;
    public InputField ipPassWord;


    public Button loginButton;
    public Button registerButton;


    public TextMeshProUGUI textProfile;
    public GameObject panel;


    public DatabaseReference reference;
    public Button butttonUploadData;
    public Button butttonReadData;


    private void Start()
    {
        auth = FirebaseAuth.DefaultInstance;


        reference = FirebaseDatabase.DefaultInstance.RootReference;


        loginButton.onClick.AddListener(LoginWithEmailPassWorld);
        registerButton.onClick.AddListener(RegisterWithEmailPassWorld);
        butttonUploadData.onClick.AddListener(WriteDataToFirebase);
        butttonReadData.onClick.AddListener(ReadData);

    }

    private void RegisterWithEmailPassWorld()
    {
        string email = ipEmail.text;
        string password = ipPassWord.text;


        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log(task.Exception);
                return;
            } 
            if (task.IsFaulted)
            {
                Debug.Log(task.Exception);
                return;
            }
            if (task.IsCompleted)
            {
                Debug.Log("tao thanh cong");
                return;
            }
        });
    }

    private void LoginWithEmailPassWorld()
    {
        string email = ipEmail.text;
        string password = ipPassWord.text;

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled) return;
            if (task.IsFaulted) return;


            if (task.IsCompleted)
            {

                Debug.Log("dang nhap thanh cong");

                var user = task.Result;

                //panel.gameObject.SetActive(false);

                return;
            }
        });      
    }


    public void WriteDataToFirebase()
    {
        reference.Child("users").Child(auth.CurrentUser.UserId).SetValueAsync("XinChao");
    }


    public void ReadData()
    {
        reference.Child("users").Child(auth.CurrentUser.UserId).GetValueAsync().ContinueWith( task =>
        {
            if(task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                Debug.Log(snapshot.Value.ToString());
            }
        });
    }
}
