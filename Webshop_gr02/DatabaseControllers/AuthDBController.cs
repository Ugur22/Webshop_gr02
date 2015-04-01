using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webshop_gr02.Models;
using WorkshopASPNETMVC3_IV_.Models;

namespace Webshop_gr02.DatabaseControllers
{
    public class AuthDBController
    {
        private MySqlConnection conn;

        public AuthDBController()
        {
            conn = new MySqlConnection("Server=localhost;Database=intosport;Uid=root;Pwd=;");
        }

        public void InsertCategorie(Categorie categorie)
        {
            MySqlTransaction trans = null;
            try
            {
                conn.Open();
                trans = conn.BeginTransaction();
                String insertString = @"insert into categorie(naam)
                values (@naam)";
                MySqlCommand cmd = new MySqlCommand(insertString, conn);
                MySqlParameter naamParam = new MySqlParameter("@naam", MySqlDbType.VarChar);

                naamParam.Value = categorie.Naam;

                cmd.Parameters.Add(naamParam);

                cmd.Prepare();

                cmd.ExecuteNonQuery();

                trans.Commit();
            }
            catch (Exception e)
            {
                trans.Rollback();
                Console.Write("Categorie niet toegevoegd: " + e);
            }
            finally
            {
                conn.Close();
            }
        }

        public bool isAuthorized(string usernaam, string password)
        {
            try
            {
                conn.Open();

                string selectQueryStudent = @"select * from gebruiker where username = @username and password = @password";

                MySqlCommand cmd = new MySqlCommand(selectQueryStudent, conn);

                MySqlParameter usernameParam = new MySqlParameter("@username", MySqlDbType.VarChar);
                MySqlParameter passwordParam = new MySqlParameter("@password", MySqlDbType.VarChar);
                usernameParam.Value = usernaam;
                passwordParam.Value = password;
                cmd.Parameters.Add(usernameParam);
                cmd.Parameters.Add(passwordParam);
                cmd.Prepare();

                MySqlDataReader dataReader = cmd.ExecuteReader();

                return dataReader.Read();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        public string[] getRollen(string username)
        {
            try
            {
                conn.Open();

                string selectQueryStudent = @"select rol_naam 
                                                from rol r, gebruiker g, rol_gebruiker rg 
                                                where r.rol_id = rg.rol_ID and g.ID_G = rg.ID_G and g.username = @username;";

                MySqlCommand cmd = new MySqlCommand(selectQueryStudent, conn);

                MySqlParameter usernameParam = new MySqlParameter("@username", MySqlDbType.VarChar);
                usernameParam.Value = username;
                cmd.Parameters.Add(usernameParam);
                cmd.Prepare();

                MySqlDataReader dataReader = cmd.ExecuteReader();

                List<string> rollen = new List<string>();
                while (dataReader.Read())
                {
                    string rolnaam = dataReader.GetString("rol_naam");
                    rollen.Add(rolnaam);
                }
                return rollen.ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new string[] { };
            }
            finally
            {
                conn.Close();
            }
        }

        public List<Categorie> GetCategorieën()
        {
            List<Categorie> categorieën = new List<Categorie>();
            try
            {
                conn.Open();

                string selectQuery = @"select * from categorie";

                MySqlCommand cmd = new MySqlCommand(selectQuery, conn);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    int ID_C = dataReader.GetInt32("ID_C");
                    string naam = dataReader.GetString("naam");


                    Categorie categorie = new Categorie { ID_C = ID_C, Naam = naam };
                    categorieën.Add(categorie);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ophalen van categorieën mislukt" + e);

            }
            finally
            {
                conn.Close();
            }
            return categorieën;
        }

        public void InsertRegistratie(Registratie registratie)
        {
            MySqlTransaction trans = null;
            try
            {
                conn.Open();
                trans = conn.BeginTransaction();
                String insertString = @"insert into gebruiker(voornaam, tussenvoegsel, achternaam, username, password, email, geslacht)
                values (@voornaam, @tussenvoegsel, @achternaam, @username, @password, @email, @geslacht)";
                MySqlCommand cmd = new MySqlCommand(insertString, conn);
                MySqlParameter voornaamParam = new MySqlParameter("@voornaam", MySqlDbType.VarChar);
                MySqlParameter tussenvoegselParam = new MySqlParameter("@tussenvoegsel", MySqlDbType.VarChar);
                MySqlParameter achternaamParam = new MySqlParameter("@achternaam", MySqlDbType.VarChar);
                MySqlParameter usernameParam = new MySqlParameter("@username", MySqlDbType.VarChar);
                MySqlParameter passwordParam = new MySqlParameter("@password", MySqlDbType.VarChar);
                MySqlParameter emailParam = new MySqlParameter("@email", MySqlDbType.VarChar);
                MySqlParameter geslachtParam = new MySqlParameter("@geslacht", MySqlDbType.VarChar);

                voornaamParam.Value = registratie.Voornaam;
                tussenvoegselParam.Value = registratie.Tussenvoegsel;
                achternaamParam.Value = registratie.Achternaam;
                usernameParam.Value = registratie.Username;
                passwordParam.Value = registratie.Password;
                emailParam.Value = registratie.Email;
                geslachtParam.Value = registratie.Geslacht;

                cmd.Parameters.Add(voornaamParam);
                cmd.Parameters.Add(tussenvoegselParam);
                cmd.Parameters.Add(achternaamParam);
                cmd.Parameters.Add(usernameParam);
                cmd.Parameters.Add(passwordParam);
                cmd.Parameters.Add(emailParam);
                cmd.Parameters.Add(geslachtParam);

                cmd.Prepare();

                cmd.ExecuteNonQuery();

                trans.Commit();
            }
            catch (Exception e)
            {
                trans.Rollback();
                Console.Write("Gebruiker niet toegevoegd: " + e);
            }
            finally
            {
                conn.Close();
            }
        }

        public void InsertProductType(ProductType productType)
        {

            MySqlTransaction trans = null;
            try
            {
                conn.Open();
                trans = conn.BeginTransaction();
				
                String insertString = @"insert into product_type(naam, inkoop_prijs, verkoop_prijs , omschrijving, image_path, zichtbaar, aanbieding, merk)
                values (@naam, @inkoop_prijs, @verkoop_prijs, @omschrijving, @image_path, @zichtbaar, @aanbieding, @merk)";

                MySqlCommand cmd = new MySqlCommand(insertString, conn);
                MySqlParameter naamParam = new MySqlParameter("@naam", MySqlDbType.VarChar);
                MySqlParameter inkoopPrijsParam = new MySqlParameter("@inkoop_prijs", MySqlDbType.Float);
                MySqlParameter verkoopPrijsParam = new MySqlParameter("@verkoop_prijs", MySqlDbType.Float);
                MySqlParameter omschrijvingParam = new MySqlParameter("@omschrijving", MySqlDbType.VarChar);
                MySqlParameter image_path = new MySqlParameter("@image_path", MySqlDbType.VarChar);
                MySqlParameter zichtbaarParam = new MySqlParameter("@zichtbaar", MySqlDbType.Int32);
                MySqlParameter aanbiedingParam = new MySqlParameter("@aanbieding", MySqlDbType.Double);
                MySqlParameter merkParam = new MySqlParameter("@merk", MySqlDbType.VarChar);

                naamParam.Value = productType.Naam;
                inkoopPrijsParam.Value = productType.InkoopPrijs;
                verkoopPrijsParam.Value = (productType.VerkoopPrijs);
                omschrijvingParam.Value = productType.Omschrijving;
                image_path.Value = productType.image_path;
                zichtbaarParam.Value = productType.Zichtbaar;
                aanbiedingParam.Value = productType.Aanbieding;
                merkParam.Value = productType.Merk;

                if (productType.Aanbieding > 0)
                {
                    Console.Write("gelukt");
                verkoopPrijsParam.Value = ((100-productType.Aanbieding)/100) * productType.VerkoopPrijs;
                }

                cmd.Parameters.Add(naamParam);
                cmd.Parameters.Add(inkoopPrijsParam);
                cmd.Parameters.Add(verkoopPrijsParam);
                cmd.Parameters.Add(omschrijvingParam);
                cmd.Parameters.Add(image_path);
                cmd.Parameters.Add(zichtbaarParam);
                cmd.Parameters.Add(aanbiedingParam);
                cmd.Parameters.Add(merkParam);

                cmd.Prepare();

                cmd.ExecuteNonQuery();

                trans.Commit();
            }
            catch (Exception e)
            {
                trans.Rollback();
                Console.Write("Gebruiker niet toegevoegd: " + e);
            }
            finally
            {
                conn.Close();
            }
        }

        public void InsertProduct(Product product)
        {
            MySqlTransaction trans = null;
            try
            { 
                conn.Open();
                trans = conn.BeginTransaction();

                String insertString = @"INSERT INTO product(naam, voorraad, zichtbaar) values (@naam, @voorraad, @zichtbaar)";

                MySqlCommand cmd = new MySqlCommand(insertString, conn);
                MySqlParameter naamParam = new MySqlParameter("@naam", MySqlDbType.VarChar);
                MySqlParameter voorraadParam = new MySqlParameter("@voorraad", MySqlDbType.Int32);
                MySqlParameter zichtbaarParam = new MySqlParameter("@zichtbaar", MySqlDbType.Int32);

                naamParam.Value = product.naam;
                voorraadParam.Value = product.voorraad;
                zichtbaarParam.Value = product.zichtbaar;

                cmd.Parameters.Add(naamParam);
                cmd.Parameters.Add(voorraadParam);
                cmd.Parameters.Add(zichtbaarParam);

                cmd.Prepare();

                cmd.ExecuteNonQuery();

                trans.Commit();
            }
            catch (Exception e)
            {
                trans.Rollback();
                Console.Write("Gebruiker niet toegevoegd: " + e);
            }
            finally
            {
                conn.Close();
            }
        }

        public List<Product> getTotalOmzet()
        {
            DateTime today = DateTime.Now;
            DateTime answer = today.AddMonths(-12);

            List<Product> producten = new List<Product>();
            int productId = 0;
            string productName = "";
            double brutoOmzet = 0;
            double nettoOmzet = 0;

            try
            {
                conn.Open();

                string selectQueryOmzetMonthly = @"SELECT pt.ID_PT as Product_ID, pt.naam as Naam,
                                                    (pt.verkoop_prijs*count(vp.ID_P)) as BRUTO_omzet, 
                                                    ((pt.verkoop_prijs-pt.inkoop_prijs)*count(vp.ID_P)) as NETTO_omzet
                                                    FROM product_type pt left join product p on pt.ID_PT = p.ID_PT

                                                   left join verkocht_product vp on p.ID_P = vp.ID_P
                                                   where  vp.verkoop_datum between @firstDate and @secondDate                                                   
                                                   GROUP BY pt.ID_PT;";
                MySqlCommand cmd = new MySqlCommand(selectQueryOmzetMonthly, conn);

                MySqlParameter firstDateParam = new MySqlParameter("@firstDate", MySqlDbType.VarChar);
                MySqlParameter secondDateParam = new MySqlParameter("@secondDate", MySqlDbType.VarChar);
                
                firstDateParam.Value = answer.ToString("yyyy/MM") + "/01";
                secondDateParam.Value = today.ToString("yyyy/MM") + "/01";

                cmd.Parameters.Add(firstDateParam);
                cmd.Parameters.Add(secondDateParam);
                cmd.Prepare();

                cmd.ExecuteNonQuery();

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    productId = dataReader.GetInt32("Product_ID");
                    productName = dataReader.GetString("Naam");
                    brutoOmzet = dataReader.GetDouble("BRUTO_omzet");
                    nettoOmzet = dataReader.GetDouble("NETTO_omzet");

                    Product product = new Product { naam = productName, BrutoOmzet = brutoOmzet, NettoOmzet = nettoOmzet };

                    producten.Add(product);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                conn.Close();
            }
            return producten;
        }

        public List<Product> getMonthlyOmzet(string date)
        {
            List<Product> producten = new List<Product>();
            int productId = 0;
            string productName = "";
            double brutoOmzet = 0;
            double nettoOmzet = 0;

            try
            {
                conn.Open();

                string selectQueryOmzetMonthly = @"SELECT pt.ID_PT as Product_ID, pt.naam as Naam,
                                                    (pt.verkoop_prijs*count(vp.ID_P)) as BRUTO_omzet, 
                                                    ((pt.verkoop_prijs-pt.inkoop_prijs)*count(vp.ID_P)) as NETTO_omzet
                                                    FROM product_type pt left join product p on pt.ID_PT = p.ID_PT

                                                   left join verkocht_product vp on p.ID_P = vp.ID_P
                                                   where  vp.verkoop_datum between @firstDate and @secondDate                                                   
                                                   GROUP BY pt.ID_PT;";
                MySqlCommand cmd = new MySqlCommand(selectQueryOmzetMonthly, conn);

                MySqlParameter firstDateParam = new MySqlParameter("@firstDate", MySqlDbType.VarChar);
                MySqlParameter secondDateParam = new MySqlParameter("@secondDate", MySqlDbType.VarChar);

                firstDateParam.Value = date + "/01";
                secondDateParam.Value = date + "/31";

                cmd.Parameters.Add(firstDateParam);
                cmd.Parameters.Add(secondDateParam);
                cmd.Prepare();

                cmd.ExecuteNonQuery();

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    productId = dataReader.GetInt32("Product_ID");
                    productName = dataReader.GetString("Naam");
                    brutoOmzet = dataReader.GetDouble("BRUTO_omzet");
                    nettoOmzet = dataReader.GetDouble("NETTO_omzet");

                    Product product = new Product { naam = productName, BrutoOmzet = brutoOmzet, NettoOmzet = nettoOmzet };

                    producten.Add(product);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                conn.Close();
            }
            return producten;
        }

        public List<Product> GetProductTop10()
        {
            List<Product> producten = new List<Product>();
            string naamProduct = "";
            double prijsProduct = 0;
            int afzet = 0;

            try
            {
                conn.Open();

                string selectQuery = @"SELECT pt.ID_PT as Product_ID, pt.Naam as Naam, count(vp.ID_P) as Afzet, pt.verkoop_prijs as Prijs
                                        FROM product_type pt left join product p on pt.ID_PT = p.ID_PT
                                                             left join verkocht_product vp on p.ID_P = vp.ID_P
                                        GROUP BY pt.ID_PT
                                        order by afzet desc, Product_ID 
                                        limit 10;";

                MySqlCommand cmd = new MySqlCommand(selectQuery, conn);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {

                    naamProduct = dataReader.GetString("Naam");
                    prijsProduct = dataReader.GetDouble("Prijs");
                    afzet = dataReader.GetInt32("Afzet");

                    Product product = new Product { naam = naamProduct, afzet = afzet, prijs = prijsProduct };

                    producten.Add(product);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                conn.Close();
            }
            return producten;
        }

        public List<Product> getMonthlyProductTop10(string date)
        {
            List<Product> producten = new List<Product>();
            int productID = 0;
            string naamProduct = "";
            double prijsProduct = 0;
            int afzet = 0;

            try
            {
                conn.Open();

                string selectQuery = @"SELECT pt.ID_PT as Product_ID, pt.Naam as Naam, count(vp.ID_P) as Afzet, pt.verkoop_prijs as Prijs
                                        FROM product_type pt left join product p on pt.ID_PT = p.ID_PT
                                                             left join verkocht_product vp on p.ID_P = vp.ID_P
                                         WHERE vp.verkoop_datum between @firstDate and @secondDate
                                        GROUP BY pt.ID_PT
                                        order by afzet desc, Product_ID 
                                        limit 10;";

                MySqlCommand cmd = new MySqlCommand(selectQuery, conn);

                MySqlParameter firstDateParam = new MySqlParameter("@firstDate", MySqlDbType.VarChar);
                MySqlParameter secondDateParam = new MySqlParameter("@secondDate", MySqlDbType.VarChar);
                firstDateParam.Value = date + "/01";
                secondDateParam.Value = date + "/31";

                cmd.Parameters.Add(firstDateParam);
                cmd.Parameters.Add(secondDateParam);
                cmd.Prepare();

                cmd.ExecuteNonQuery();

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    productID = dataReader.GetInt32("Product_ID");
                    naamProduct = dataReader.GetString("Naam");
                    prijsProduct = dataReader.GetDouble("Prijs");
                    afzet = dataReader.GetInt32("Afzet");
                    Product product = new Product { naam = naamProduct, afzet = afzet, prijs = prijsProduct };

                    producten.Add(product);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                conn.Close();
            }
            return producten;
        }

        public List<Product> GetProductBottom10()
        {
            List<Product> producten = new List<Product>();
            int productID = 0;
            string naamProduct = "";
            double prijsProduct = 0;
            int afzet = 0;

            try
            {
                conn.Open();

                string selectQuery = @"SELECT pt.ID_PT as Product_ID, pt.Naam as Naam, count(vp.ID_P) as Afzet, pt.verkoop_prijs as Prijs
                                        FROM product_type pt left join product p on pt.ID_PT = p.ID_PT
                                                             left join verkocht_product vp on p.ID_P = vp.ID_P
                                        GROUP BY pt.ID_PT
                                        order by afzet asc, Product_ID 
                                        limit 10;";

                MySqlCommand cmd = new MySqlCommand(selectQuery, conn);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    productID = dataReader.GetInt32("Product_ID");
                    naamProduct = dataReader.GetString("Naam");
                    prijsProduct = dataReader.GetDouble("Prijs");
                    afzet = dataReader.GetInt32("Afzet");
                    Product product = new Product { naam = naamProduct, afzet = afzet, prijs = prijsProduct };

                    producten.Add(product);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                conn.Close();
            }
            return producten;
        }

        public List<Product> GetMonthlyProductBottom10(string date)
        {
            List<Product> producten = new List<Product>();
            int productID = 0;
            string naamProduct = "";
            double prijsProduct = 0;
            int afzet = 0;

            try
            {
                conn.Open();

                string selectQuery = @"SELECT pt.ID_PT as Product_ID, pt.Naam as Naam, count(vp.ID_P) as Afzet, pt.verkoop_prijs as Prijs
                                        FROM product_type pt 
                                        
                                                             left join product p on pt.ID_PT = p.ID_PT
                                                             left join verkocht_product vp on p.ID_P = vp.ID_P
                                         WHERE vp.verkoop_datum between @firstDate and @secondDate
                                        GROUP BY pt.ID_PT
                                        order by afzet asc, Product_ID 
                                        limit 10;";

                MySqlCommand cmd = new MySqlCommand(selectQuery, conn);

                MySqlParameter firstDateParam = new MySqlParameter("@firstDate", MySqlDbType.VarChar);
                MySqlParameter secondDateParam = new MySqlParameter("@secondDate", MySqlDbType.VarChar);
                firstDateParam.Value = date + "/01";
                secondDateParam.Value = date + "/31";

                cmd.Parameters.Add(firstDateParam);
                cmd.Parameters.Add(secondDateParam);
                cmd.Prepare();

                cmd.ExecuteNonQuery();

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    productID = dataReader.GetInt32("Product_ID");
                    naamProduct = dataReader.GetString("Naam");
                    prijsProduct = dataReader.GetDouble("Prijs");
                    afzet = dataReader.GetInt32("Afzet");
                    Product product = new Product { naam = naamProduct, afzet = afzet, prijs = prijsProduct };

                    producten.Add(product);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                conn.Close();
            }
            return producten;
        }

        public List<ProductType> GetTypeLijst()
        {
            List<ProductType> productenType = new List<ProductType>();
            int ID_PT;
            string naamProduct;
            String omschrijving;
            String imagePath;
            int zichtbaar;
            double aanbieding;
            float inkoopPrijs;
            float verkoopPrijs;
            String merk;


            try
            {
                conn.Open();

                string selectQuery = @"select * from product_type";

                MySqlCommand cmd = new MySqlCommand(selectQuery, conn);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    ID_PT = dataReader.GetInt16("ID_PT");
                    naamProduct = dataReader.GetString("naam");
                    inkoopPrijs = dataReader.GetFloat("inkoop_prijs");
                    verkoopPrijs = dataReader.GetFloat("verkoop_prijs");
                    omschrijving = dataReader.GetString("omschrijving");
                    imagePath = dataReader.GetString("image_path");
                    zichtbaar = dataReader.GetInt32("zichtbaar");
                    aanbieding = dataReader.GetDouble("aanbieding");
                    merk = dataReader.GetString("merk");

                    ProductType productType = new ProductType { ID_PT= ID_PT, Naam = naamProduct, InkoopPrijs = inkoopPrijs, VerkoopPrijs = verkoopPrijs, Omschrijving = omschrijving, image_path = imagePath, Aanbieding = aanbieding, Zichtbaar = zichtbaar, Merk= merk };
                    productenType.Add(productType);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ophalen van typeProduct mislukt" + e);
            }
            finally
            {
                conn.Close();
            }
            return productenType;
        }

        public List<Product> GetProductLijst()
        {
            List<Product> productenLijst = new List<Product>();

            int ID_P = 0;
            string naam = "";
            int voorraad = 0;
            int zichtbaar = 0;
            int ID_PT = 0;
            string naamPT = "";
           

            try
            {
                conn.Open();

                string selectQuery = @"select p.ID_p as ID_P, p.naam as naam, p.voorraad as voorraad, p.zichtbaar as zichtbaar, pt.ID_PT as ID_PT, pt.naam as naam_producttype from product p
                                        left join product_type pt on p.ID_PT = pt.ID_PT
                                        group by p.ID_P;
                                        ";

                MySqlCommand cmd = new MySqlCommand(selectQuery, conn);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    ID_P = dataReader.GetInt32("ID_P");
                    naam = dataReader.GetString("naam");
                    voorraad = dataReader.GetInt32("voorraad");
                    zichtbaar = dataReader.GetInt32("zichtbaar");
                    ID_PT = dataReader.GetInt32("ID_PT");
                    naamPT = dataReader.GetString("naam_producttype");
                    

                    ProductType productType = new ProductType { ID_PT = ID_PT, Naam = naamPT};
                    Product product = new Product { ID_P = ID_P, naam = naam, voorraad = voorraad, zichtbaar = zichtbaar, productType = productType };
                    productenLijst.Add(product);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ophalen van typeProduct mislukt" + e);
            }
            finally
            {
                conn.Close();
            }
            return productenLijst;
        }

        public void verwijderProductType(string ProductId)
        {
            Console.WriteLine(ProductId);
            MySqlTransaction trans = null;
            try
            {
                conn.Open();
                trans = conn.BeginTransaction();

                String DeleteProductTypeString = @"DELETE FROM product_type WHERE ID_PT = @productID";

                MySqlCommand cmd = new MySqlCommand(DeleteProductTypeString, conn);
                MySqlParameter IdParam = new MySqlParameter("@productID", MySqlDbType.Int32);

                IdParam.Value = ProductId;

                cmd.Parameters.Add(IdParam);

                cmd.Prepare();

                cmd.ExecuteNonQuery();

                trans.Commit();
            }
            catch (Exception e)
            {
                trans.Rollback();
                Console.Write("Gebruiker niet toegevoegd: " + e);
            }
            finally
            {
                conn.Close();
            }
        }

        public void verwijderProduct(string ProductId)
        {
            Console.WriteLine(ProductId);
            MySqlTransaction trans = null;
            try
            {
                conn.Open();
                trans = conn.BeginTransaction();

                String DeleteProductTypeString = @"DELETE FROM product WHERE ID_P = @ProductID";

                MySqlCommand cmd = new MySqlCommand(DeleteProductTypeString, conn);
                MySqlParameter IdParam = new MySqlParameter("@ProductID", MySqlDbType.Int32);

                IdParam.Value = ProductId;

                cmd.Parameters.Add(IdParam);

                cmd.Prepare();

                cmd.ExecuteNonQuery();

                trans.Commit();
            }
            catch (Exception e)
            {
                trans.Rollback();
                Console.Write("Gebruiker niet toegevoegd: " + e);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
