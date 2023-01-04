package com.example.cenywpolsce.data;

import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;

import androidx.annotation.Nullable;

import com.example.cenywpolsce.models.PriceChange;
import com.example.cenywpolsce.models.Product;

import java.util.ArrayList;

public class DbContext extends SQLiteOpenHelper {
    protected static final String DB_NAZWA = "/storage/emulated/0/MojFolder/ceny.db";
    protected static final int DB_WERSJA = 1;

    protected ArrayList<PriceChange> priceChanges;

    protected SQLiteDatabase db;

    public DbContext(@Nullable Context context) {
        super(context, DB_NAZWA, null, DB_WERSJA);

        db = getReadableDatabase();
    }

    @Override
    public void onCreate(SQLiteDatabase db) {
        //Tutaj nic nie robimy, dlatego że korzystamy z bazy wygenerowanej przez .NET Core
    }

    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {

    }

    public int count() {
        Cursor cursor = db.rawQuery("SELECT COUNT(Id) FROM Products;", null);
        cursor.moveToNext();

        return cursor.getInt(0);
    }

    public int productsCount() {
        Cursor cursor = db.rawQuery("SELECT COUNT(DISTINCT Nazwa) FROM Products;", null);
        cursor.moveToNext();

        return cursor.getInt(0);
    }

    public ArrayList<String> search(String term) {
        if (term.length() == 0)
            term = "%";
        else
            term = "%" + term.toLowerCase() + "%";

        Cursor c = db.rawQuery("SELECT DISTINCT Nazwa FROM Products WHERE LOWER(Nazwa) LIKE '" + term.toLowerCase() + "' ORDER BY Nazwa;", null);
        ArrayList<String> products = new ArrayList<>();

        while (c.moveToNext()) {
            products.add(c.getString(c.getColumnIndexOrThrow("Nazwa")));
        }

        return products;
    }

    public ArrayList<Product> get(String name) {
        Cursor c = db.rawQuery("SELECT * FROM Products WHERE Nazwa = '" + name + "' ORDER BY Data;", null);
        ArrayList<Product> products = new ArrayList<>();

        while (c.moveToNext()) {
            products.add(new Product(c));
        }

        return products;
    }

    public ArrayList<Product> getAll() {
        Cursor c = db.rawQuery("SELECT * FROM Products ORDER BY Nazwa;", null);
        ArrayList<Product> products = new ArrayList<>();

        while (c.moveToNext()) {
            products.add(new Product(c));
        }

        return products;
    }

    public ArrayList<Product> getAllDistinct() {
        Cursor c = db.rawQuery("SELECT * FROM Products GROUP BY Nazwa ORDER BY Nazwa;", null);
        ArrayList<Product> products = new ArrayList<>();

        while (c.moveToNext()) {
            products.add(new Product(c));
        }

        return products;
    }

    public ArrayList<Product> mostPriceRise() {
        //Wynik to: "wywóz śmieci - za 1pojemnik"
        return get("wywóz śmieci - za 1pojemnik");
    }

    public ArrayList<Product> mostPriceFall() {
        //Wynik to: "odtwarzacz DVD"
        return get("odtwarzacz DVD");
    }
}
