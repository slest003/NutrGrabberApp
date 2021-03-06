﻿using System;
using Android.Views;
using Android.Content;
using Android.Runtime;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content.Res;
using System.IO;
using SQLite;
using System.Linq;
using Android.Database.Sqlite;
using System.Collections.Generic;
using System.Collections;

namespace nutr_grabber
{

    [Activity(Label = "nutr_grabber", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
                string str1;
        
                // Android needs a databse to be copied from assets to a useable location
        public void copyDataBase()
        {
            var dbPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "UsdDataProto.db");

            if (!System.IO.File.Exists(dbPath))
            {
                var dbAssetStream = Assets.Open("UsdDataProto.db");
                var dbFileStream = new FileStream(dbPath, FileMode.OpenOrCreate);
                var buffer = new byte[1024];

                int b = buffer.Length;
                int length;

                while ((length = dbAssetStream.Read(buffer, 0, b)) > 0)
                {
                    dbFileStream.Write(buffer, 0, length);
                }

                dbFileStream.Flush();
                dbFileStream.Close();
                dbAssetStream.Close();
            }
        }


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            // makes the database
              try
              {
                  copyDataBase();
                new AlertDialog.Builder(this)
                      .SetMessage("Database created ...")
                      .Show();
            }
              catch(Exception e)
              {

                  new AlertDialog.Builder(this)
                      .SetMessage("Database not created ...")
                      .Show();                  

              }
                       

            // Set our view from the "main" layout resource

            SetContentView(Resource.Layout.Main);


            //set widgets
            TextView message = FindViewById<TextView>(Resource.Id.message);
            EditText ingred = FindViewById<EditText>(Resource.Id.enterHere);
            Button search = FindViewById<Button>(Resource.Id.search);

            //open sqlite connection, create table
            var Path = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "UsdDataProto.db");
            var db = new SQLiteConnection(Path);
            db.CreateTable<usdProto>();
            

                
            search.Click += (object sender, EventArgs e) =>
                    {
                        str1 = ingred.Text;


                        var query = db.Query<usdProto>("SELECT Energ_Kcal FROM usdProto WHERE Shrt_Desc = ?", str1);

                     
                        //foreach (var item in query)
                        //{
                            new AlertDialog.Builder(this)
                                    .SetMessage(query)
                                    .Show();
                        //}
                    };
                    

            /*
             using (var conn = new SQLite.SQLiteConnection(usd))
             {
                 conn.CreateTable<usdProto>();

                  var Item = conn.Table<usdProto>().Where(v => v.Shrt_Desc.Contains(str1));
                 new AlertDialog.Builder(this)
              .SetMessage(Item.ToString())
              .Show();
             }
            */
            /*
                           var Item = db.Query<usdProto>("SELECT Energ_Kcal FROM usdProto WHERE Shrt_Desc = ?", str1);
                            db.Close();
                            /*  string content;
                               AssetManager assets = this.Assets;
                               using (StreamReader sr = new StreamReader(assets.Open("UsdDataProto.db")))
                               {
                                   content = sr.ReadLine();
                               }

                            new AlertDialog.Builder(this)
                            .SetMessage(Item.ToString())
                            .Show();
            */
        }



//----------------------------------------------------------------------------
        //[Table("usdProto")]
        public class usdProto
        {
           
            public int NDB_No { get; set; }
            [PrimaryKey]
            public string Shrt_Desc { get; set; }
            public int Energ_Kcal { get; set; }           
            public int Protein_g { get; set; }
            public int Lipid_Tot_g { get; set; }
            public int Ash_g { get; set; }
            public int Carbohydrt_g { get; set; }
            public int Fiber_TD_g { get; set; }
            public int Sugar_Tot_g { get; set; }
            public int Calcium_mg { get; set; }
            public int Iron_mg { get; set; }
            public int Magnesium_mg { get; set; }
            public int Phosphorus_mg { get; set; }
            public int Potassium_mg { get; set; }
            public int Sodium_mg { get; set; }
            public int Zinc_mg { get; set; }
            public int Copper_mg { get; set; }
            public int Manganese_mg { get; set; }
            public int Selenium_ug { get; set; }
            public int Vit_C_mg { get; set; }
            public int Thiamin_mg { get; set; }
            public int Riboflavin_mg { get; set; }
            public int Niacin_mg { get; set; }
            public int Panto_Acid_mg { get; set; }
            public int Vit_B6_mg { get; set; }
            public int Folate_Tot_ug { get; set; }
            public int Folic_Acid_ug { get; set; }
            public int Food_Folate_ug { get; set; }
            public int Folate_DFE_ug { get; set; }
            public int Choline_Tot_mg { get; set; }
            public int Vit_B12_ug { get; set; }
            public int Vit_A_IU { get; set; }
            public int Vit_A_RAE { get; set; }
            public int Retinol_ug { get; set; }
            public int Alpha_Carot_ug { get; set; }
            public int Beta_Carot_ug { get; set; }
            public int Beta_Crypt_ug { get; set; }
            public int Lycopene_ug { get; set; }
            public int Lut_Zea_ug { get; set; }
            public int Vit_E_mg { get; set; }
            public int Vit_D_ug { get; set; }
            public int Vit_D_IU { get; set; }
            public int Vit_K_ug { get; set; }
            public int FA_Sat_g { get; set; }
            public int FA_Mono_g { get; set; }
            public int FA_Poly_g { get; set; }
            public int Cholestrl_mg { get; set; }
            public int Gm_unit { get; set; }
            public int num { get; set; }
            public int unit { get; set; }

    
        }




    }
}
