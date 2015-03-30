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

                string selectQueryStudent = @"select * from categorie";

                MySqlCommand cmd = new MySqlCommand(selectQueryStudent, conn);
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

        public void InsertProductType(ProductType productType) {

            MySqlTransaction trans = null;
            try
            {
                conn.Open();
                trans = conn.BeginTransaction();
                String insertString = @"insert into product_type(naam, inkoop_prijs, verkoop_prijs , omschrijving, image_path, zichtbaar, aanbieding)
                values (@naam, @inkoop_prijs, @verkoop_prijs, @omschrijving, @image_path, @zichtbaar, @aanbieding)";
                MySqlCommand cmd = new MySqlCommand(insertString, conn);
                MySqlParameter naamParam = new MySqlParameter("@naam", MySqlDbType.VarChar);
                MySqlParameter inkoopPrijsParam = new MySqlParameter("@inkoop_prijs", MySqlDbType.Float);
                MySqlParameter verkoopPrijsParam = new MySqlParameter("@verkoop_prijs", MySqlDbType.Float);
                MySqlParameter omschrijvingParam = new MySqlParameter("@omschrijving", MySqlDbType.VarChar);
                MySqlParameter image_path = new MySqlParameter("@image_path", MySqlDbType.VarChar);
                MySqlParameter zichtbaarParam = new MySqlParameter("@zichtbaar", MySqlDbType.VarChar);
                MySqlParameter aanbiedingParam = new MySqlParameter("@aanbieding", MySqlDbType.VarChar);

                naamParam.Value = productType.Naam;
                inkoopPrijsParam.Value = productType.InkoopPrijs;
                verkoopPrijsParam.Value = (productType.VerkoopPrijs);
                omschrijvingParam.Value = productType.Omschrijving;
                image_path.Value = productType.image_path;
                zichtbaarParam.Value = productType.Zichtbaar;
                aanbiedingParam.Value = productType.Aanbieding;

                cmd.Parameters.Add(naamParam);
                cmd.Parameters.Add(inkoopPrijsParam);
                cmd.Parameters.Add(verkoopPrijsParam);
                cmd.Parameters.Add(omschrijvingParam);
                cmd.Parameters.Add(image_path);
                cmd.Parameters.Add(zichtbaarParam);
                cmd.Parameters.Add(aanbiedingParam);

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
                                                    (pt.verkoop_prijs*count(vp.ID_PT)) as BRUTO_omzet, 
                                                    ((pt.verkoop_prijs-pt.inkoop_prijs)*count(vp.ID_PT)) as NETTO_omzet
                                                    FROM product_type pt left join verkocht_product vp on pt.ID_PT = vp.ID_PT
                                                    where vp.verkoop_datum between @firstDate and @secondDate
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

                    Product product = new Product { ID = productId, naam = productName, BrutoOmzet = brutoOmzet, NettoOmzet = nettoOmzet };

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
            int productID = 0;
            string naamProduct = "";
            double prijsProduct = 0;
            int afzet = 0;

            try
            {
                conn.Open();

                string selectQuery = @"SELECT p.ID_PT as Product_ID, p.Naam as Naam, count(vp.ID_PT) as Afzet, p.verkoop_prijs as Prijs
                                        FROM product_type p left join verkocht_product vp on p.ID_PT = vp.ID_PT
                                        GROUP BY p.ID_PT
                                        order by afzet desc, Product_ID 
                                        limit 10;"
                                        ;
                
                MySqlCommand cmd = new MySqlCommand(selectQuery, conn);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    productID = dataReader.GetInt32("Product_ID");
                    naamProduct = dataReader.GetString("Naam");
                    prijsProduct = dataReader.GetDouble("Prijs");
                    afzet = dataReader.GetInt32("Afzet");
                    Product product = new Product { ID = productID, naam = naamProduct, afzet = afzet, prijs = prijsProduct};

                    producten.Add(product);
                    //Console.WriteLine("" + ProductId + ProductName + BrutoOmzet + NettoOmzet);
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

            //string x = ProductId.ToString() + ProductName + BrutoOmzet.ToString() + NettoOmzet.ToString();


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

                string selectQuery = @"SELECT p.ID_P as Product_ID, p.Naam as Naam, count(vp.ID_P) as Afzet, p.verkoop_prijs as Prijs
                                        FROM product p left join verkocht_product vp on p.ID_P = vp.ID_P
                                        GROUP BY p.ID_P
                                        order by afzet asc, Product_ID 
                                        limit 10;"
                                        ;

                MySqlCommand cmd = new MySqlCommand(selectQuery, conn);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    productID = dataReader.GetInt32("Product_ID");
                    naamProduct = dataReader.GetString("Naam");
                    prijsProduct = dataReader.GetDouble("Prijs");
                    afzet = dataReader.GetInt32("Afzet");
                    Product product = new Product { ID = productID, naam = naamProduct, afzet = afzet, prijs = prijsProduct };

                    producten.Add(product);
                    //Console.WriteLine("" + ProductId + ProductName + BrutoOmzet + NettoOmzet);
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

            //string x = ProductId.ToString() + ProductName + BrutoOmzet.ToString() + NettoOmzet.ToString();


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
                                                    (pt.verkoop_prijs*count(vp.ID_PT)) as BRUTO_omzet, 
                                                    ((pt.verkoop_prijs-pt.inkoop_prijs)*count(vp.ID_PT)) as NETTO_omzet
                                                    FROM product_type pt left join verkocht_product vp on pt.ID_PT = vp.ID_PT
                                                    where vp.verkoop_datum between @firstDate and @secondDate
                                                    GROUP BY pt.ID_PT;";
                MySqlCommand cmd = new MySqlCommand(selectQueryOmzetMonthly, conn);

                MySqlParameter firstDateParam = new MySqlParameter("@firstDate", MySqlDbType.VarChar);
                MySqlParameter secondDateParam = new MySqlParameter("@secondDate", MySqlDbType.VarChar);
                firstDateParam.Value = date+"/01";
                secondDateParam.Value = date+"/31";

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

                    Product product = new Product { ID = productId, naam = productName, BrutoOmzet = brutoOmzet, NettoOmzet = nettoOmzet };

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
    }
}
