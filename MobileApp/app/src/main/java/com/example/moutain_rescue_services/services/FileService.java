package com.example.moutain_rescue_services.services;

import android.content.Context;

import com.example.moutain_rescue_services.common.GlobalConstants;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class FileService {

    private Context context;

    public FileService(Context context){
        this.context = context;
    }



    public void SaveUserRegisterInfoFile(String phoneNumber, String secretKey) {

        String saveUserInfo = phoneNumber + "\n" + secretKey;
        try {
            FileOutputStream fos = context.openFileOutput(GlobalConstants.UserFile, Context.MODE_PRIVATE);
            fos.write(saveUserInfo.getBytes());
            fos.close();
        } catch (Exception e) {
            e.printStackTrace();
        }

    }

    public String ReadUserInfo(String fileName) {

        try {
            BufferedReader inputReader = new BufferedReader(new InputStreamReader(
                    context.openFileInput(fileName)));
            String inputString;
            StringBuilder stringBuffer = new StringBuilder();
            while ((inputString = inputReader.readLine()) != null) {
                stringBuffer.append(inputString + "\n");
            }
            return stringBuffer.toString();
        } catch (IOException e) {
            e.printStackTrace();
            return "empty";
        }
    }

    public void SaveToken(String token) {
        String fileContents = token;
        FileOutputStream outputStream;

        try {
            outputStream = context.openFileOutput(GlobalConstants.UserFile, Context.MODE_PRIVATE);
            outputStream.write(fileContents.getBytes());
            outputStream.close();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public boolean IsFileEmpty(String fileName) {
        String info = "";
        boolean isEmpty = true;
        if (!IsFileExists(fileName)) {
            return isEmpty;
        }

        info = ReadUserInfo(fileName);

        if (info.equals("empty")) {
            return true;
        }

        Pattern pattern = Pattern.compile("^[0-9]+\\n");

        Matcher matcher = pattern.matcher(info);

        if (matcher.find()) {
            isEmpty = false;
        } else {
            return true;
        }

        Pattern pattern2 = Pattern.compile("\\n(.+)");

        matcher = pattern2.matcher(info);
        if (matcher.find()) {
            isEmpty = false;
        } else {
            return true;
        }

        return isEmpty;
    }


    private boolean IsFileExists(String fileName) {
        File file = new File(context.getFilesDir(), fileName);
        return file.exists();
    }


}
