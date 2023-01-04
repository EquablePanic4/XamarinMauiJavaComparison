package com.example.cenywpolsce.adapters;

import android.content.Context;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

import com.example.cenywpolsce.R;
import com.example.cenywpolsce.models.Product;

import java.util.ArrayList;
import java.util.stream.Collectors;

public class ProductListAdapter extends BaseAdapter {
    LayoutInflater mInflater;
    private ArrayList<String> products;

    public ProductListAdapter(Context context, ArrayList<String> productsList) {
        mInflater = LayoutInflater.from(context);
        products = productsList;
    }

    @Override
    public int getCount() {
        return products.size();
    }

    @Override
    public Object getItem(int position) {
        return products.get(position);
    }

    @Override
    public long getItemId(int position) {
        return position;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        ProductListBackend backend;

        if (convertView == null) {
            convertView = mInflater.inflate(R.layout.fragment_product_element, parent, false);
            backend = new ProductListBackend(convertView, products.get(position));
            convertView.setTag(backend);
        }

        else {
            backend = (ProductListBackend) convertView.getTag();
            backend.fillElement(products.get(position));
        }

        return convertView;
    }

    public void update(ArrayList<String> productList) {
        products = productList;
        notifyDataSetChanged();
    }

    class ProductListBackend {
        TextView name;

        public ProductListBackend(View view) {
            name = view.findViewById(R.id.product_name);
        }

        public ProductListBackend(View view, String product) {
            this(view);
            fillElement(product);
        }

        public void fillElement(String product) {
            name.setText(product);
        }
    }
}
