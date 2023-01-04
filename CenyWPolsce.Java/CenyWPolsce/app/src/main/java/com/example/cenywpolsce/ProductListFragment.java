package com.example.cenywpolsce;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;

import androidx.fragment.app.Fragment;

import android.text.Editable;
import android.text.TextWatcher;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.Toast;

import com.example.cenywpolsce.abstracts.DatabaseFragment;
import com.example.cenywpolsce.adapters.ProductListAdapter;
import com.example.cenywpolsce.models.Product;

import java.util.ArrayList;

public class ProductListFragment extends DatabaseFragment {

    private ProductListAdapter adapter;

    public ProductListFragment(Context context) {
        super(context);
        // Required empty public constructor
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        return inflater.inflate(R.layout.fragment_product_list, container, false);
    }

    @Override
    public void onViewCreated(View view, Bundle savedInstanceState) {
        //Wypełnianie listy produktów
        ListView productListView = view.findViewById(R.id.productListView);
        ArrayList<String> distincts = db.search("");

        adapter = new ProductListAdapter(ctx, distincts);
        productListView.setAdapter(adapter);

        //Aktywowanie szukajki
        EditText productSearch = view.findViewById(R.id.productSearch);
        productSearch.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {

            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {
                ArrayList<String> matching = db.search(s.toString());
                adapter.update(matching);
            }

            @Override
            public void afterTextChanged(Editable s) {

            }
        });

        productListView.setOnItemClickListener((parent, view1, position, id) -> {
            String name = (String) adapter.getItem(position);
            Intent intent = new Intent(ctx, ProductDetailsActivity.class);
            intent.putExtra("product", name);

            startActivity(intent);
        });
    }
}