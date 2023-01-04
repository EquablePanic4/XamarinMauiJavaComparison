package com.example.cenywpolsce.abstracts;

import android.content.Context;
import android.os.Bundle;

import androidx.fragment.app.Fragment;

import com.example.cenywpolsce.data.DbContext;

public abstract class DatabaseFragment extends Fragment {
    protected DbContext db;
    protected Context ctx;

    public DatabaseFragment(Context context) {
        ctx = context;
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        db = new DbContext(ctx);
    }
}
