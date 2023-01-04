package com.example.cenywpolsce;

import android.os.Bundle;

import com.example.cenywpolsce.abstracts.DatabaseActivity;
import com.example.cenywpolsce.adapters.ProductListAdapter;
import com.example.cenywpolsce.ui.main.SectionsPagerAdapter;
import com.google.android.material.tabs.TabLayout;

import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.app.AppCompatDelegate;
import androidx.viewpager.widget.ViewPager;

import android.widget.ListView;

public class MainActivity extends AppCompatActivity {

    private ListView products_list_view;
    private ProductListAdapter productListAdapter;

    private TabLayout tabs;
    private ViewPager viewPager;
    private SectionsPagerAdapter pagerAdapter;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        //Wyłączanie obsługi trybu nocnego
        AppCompatDelegate.setDefaultNightMode(AppCompatDelegate.MODE_NIGHT_NO);
        setContentView(R.layout.activity_main);
        SectionsPagerAdapter sectionsPagerAdapter = new SectionsPagerAdapter(this, getSupportFragmentManager());
        viewPager = findViewById(R.id.view_pager);
        viewPager.setAdapter(sectionsPagerAdapter);
        tabs = findViewById(R.id.tabs);
        tabs.setupWithViewPager(viewPager);
    }
}