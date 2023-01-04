package com.example.cenywpolsce.abstracts;

import android.os.Bundle;

import androidx.appcompat.app.AppCompatActivity;

import com.example.cenywpolsce.data.DbContext;

public abstract class DatabaseActivity extends AppCompatActivity {
    protected DbContext db;

     @Override
    protected void onCreate(Bundle savedInstanceState) {
         super.onCreate(savedInstanceState);

         db = new DbContext(this);
     }
}
