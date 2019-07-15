package com.example.moutain_rescue_services.models;

public class User {
    private String phoneNumber;
    private String smsAuthToken;
    private String authToken;

    public String GetPhoneNumber(){
     return  phoneNumber;
    }

    public void SetPhoneNumber(String phoneNumber){
        this.phoneNumber = phoneNumber;
    }

    public String  GetSmsAuthToken(){
        return  smsAuthToken;
    }

    public void SetSmsAuthToken(String smsAuthToken){
        this.smsAuthToken = smsAuthToken;
    }

    public String GetAuthToken(){
        return authToken;
    }

    public void SetAuthToken(String authToken){
        this.authToken = authToken;
    }

}
