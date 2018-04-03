using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginScreen : MonoBehaviour
{
    [SerializeField] InputField _inputFieldUsername;
    [SerializeField] InputField _inputFieldPassword;

    [SerializeField] Button _buttonLogin;
    [SerializeField] Button _buttonRegister;


    void Awake()
    {
        _buttonLogin.onClick.AddListener(() =>
        {
            if (_inputFieldUsername.text.Length > 0 && _inputFieldPassword.text.Length > 0)
                new Command.Server.UserLogin(new Command.Server.UserLogin.Data()
                {
                    registerElseLogin = false,
                    providedUsername  = _inputFieldUsername.text,
                    providedPassword  = _inputFieldPassword.text
                }).Send(NetworkManager.instance.client, NetworkManager.instance.client.ServerConnection);

            Debug.Log("Login (false)");
        });

        _buttonRegister.onClick.AddListener(() =>
        {
            if (_inputFieldUsername.text.Length > 0 && _inputFieldPassword.text.Length > 0)
                new Command.Server.UserLogin(new Command.Server.UserLogin.Data()
                {
                    registerElseLogin = true,
                    providedUsername = _inputFieldUsername.text,
                    providedPassword = _inputFieldPassword.text
                }).Send(NetworkManager.instance.client, NetworkManager.instance.client.ServerConnection);
        });
    }
}