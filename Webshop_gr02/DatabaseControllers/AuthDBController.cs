using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webshop_gr02.Models;
using WorkshopASPNETMVC3_IV_.Models;
using Webshop_gr02.ViewModels;
using CustomExtensions;
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
                String insertString = @"INSERT INTO `categorie` (`naam`) VALUES (@naam)";
                MySqlCommand cmd = new MySqlCommand(insertString, conn);
                MySqlParameter naamParam = new MySqlParameter("@naam", MySqlDbType.VarChar);

                naamParam.Value = categorie.Naam;

                cmd.Parameters.Add(naamParam);

                cmd.Prepare();

                cmd.ExecuteNonQuery();

                trans.Commit();
            }
            catch (MySqlException e)
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

                string selectQueryStudent = @"SELECT * FROM gebruiker WHERE username = @username AND password = @password";

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
            catch (MySqlException e)
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

                string selectQueryStudent = @"SELECT rolnaam 
                                              FROM rol r, gebruiker g
                                              WHERE g.ID_rol = r.rol_id AND g.username = @username;";

                MySqlCommand cmd = new MySqlCommand(selectQueryStudent, conn);

                MySqlParameter usernameParam = new MySqlParameter("@username", MySqlDbType.VarChar);
                usernameParam.Value = username;
                cmd.Parameters.Add(usernameParam);
                cmd.Prepare();

                MySqlDataReader dataReader = cmd.ExecuteReader();

                List<string> rollen = new List<string>();
                while (dataReader.Read())
                {
                    string rolnaam = dataReader.GetString("rolnaam");
                    rollen.Add(rolnaam);
                }
                return rollen.ToArray();
            }
            catch (MySqlException e)
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

                string selectQuery = @"SELECT * FROM categorie";

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
            catch (MySqlException e)
            {
                Console.WriteLine("Ophalen van categorieën mislukt" + e);

            }
            finally
            {
                conn.Close();
            }
            return categorieën;
        }

        public List<Aanbieding> GetAanbiedingen()
        {
            List<Aanbieding> aanbiedingen = new List<Aanbieding>();
            try
            {
                conn.Open();

                string selectQuery = @"SELECT * FROM aanbieding";

                MySqlCommand cmd = new MySqlCommand(selectQuery, conn);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    int ID_A = dataReader.GetInt32("ID_A");
                    string soort = dataReader.GetString("soort");
                    int percentage = dataReader.GetInt32("percentage");
                    bool actief = dataReader.GetBoolean("actief");


                    Aanbieding aanbieding = new Aanbieding { ID_A = ID_A, soort = soort, percentage = percentage, actief = actief };
                    aanbiedingen.Add(aanbieding);
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Ophalen van aanbiedingen mislukt" + e);

            }
            finally
            {
                conn.Close();
            }
            return aanbiedingen;
        }

        public void InsertRegistratie(Registratie registratie)
        {
            MySqlTransaction trans = null;
            try
            {
                conn.Open();
                trans = conn.BeginTransaction();
                String insertString = @"INSERT INTO gebruiker (voornaam, tussenvoegsel, achternaam, username, password, email, geslacht)
                VALUES (@voornaam, @tussenvoegsel, @achternaam, @username, @password, @email, @geslacht)";
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
            catch (MySqlException e)
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

                String insertString = @"INSERT INTO product_type (naam, inkoop_prijs, verkoop_prijs , omschrijving, image_path, zichtbaar, ID_A, merk)
                                        VALUES (@naam, @inkoop_prijs, @verkoop_prijs, @omschrijving, @image_path, @zichtbaar, @ID_A, @merk)";

                MySqlCommand cmd = new MySqlCommand(insertString, conn);
                MySqlParameter naamParam = new MySqlParameter("@naam", MySqlDbType.VarChar);
                MySqlParameter inkoopPrijsParam = new MySqlParameter("@inkoop_prijs", MySqlDbType.Float);
                MySqlParameter verkoopPrijsParam = new MySqlParameter("@verkoop_prijs", MySqlDbType.Float);
                MySqlParameter omschrijvingParam = new MySqlParameter("@omschrijving", MySqlDbType.VarChar);
                MySqlParameter image_path = new MySqlParameter("@image_path", MySqlDbType.VarChar);
                MySqlParameter zichtbaarParam = new MySqlParameter("@zichtbaar", MySqlDbType.Int32);
                MySqlParameter aanbiedingParam = new MySqlParameter("@ID_A", MySqlDbType.Int32);
                MySqlParameter merkParam = new MySqlParameter("@merk", MySqlDbType.VarChar);

                naamParam.Value = productType.Naam;
                inkoopPrijsParam.Value = productType.InkoopPrijs;
                verkoopPrijsParam.Value = (productType.VerkoopPrijs);
                omschrijvingParam.Value = productType.Omschrijving;
                image_path.Value = productType.ImagePath;
                zichtbaarParam.Value = productType.Zichtbaar;
                aanbiedingParam.Value = productType.Aanbieding.ID_A;
                merkParam.Value = productType.Merk;

                /* @TODO Korting toevoegen zodra een product getoond wordt aan de klant */

                if (productType.Aanbieding != null)
                {
                    Console.Write("gelukt");
                    if (productType.Aanbieding.actief)
                    {
                        verkoopPrijsParam.Value = ((100 - productType.Aanbieding.percentage) / 100) * productType.VerkoopPrijs;
                    }
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
            catch (MySqlException e)
            {
                trans.Rollback();
                Console.Write("Gebruiker niet toegevoegd: " + e);
            }
            finally
            {
                conn.Close();
            }
        }

        protected Product GetproductFromDataReader(MySqlDataReader dataReader)
        {

            int productId = dataReader.SafeGetInt32("ID_P");
            string productNaam = dataReader.SafeGetString("naam");
            int voorraad = dataReader.SafeGetInt32("voorraad");
            int zichtbaar = dataReader.SafeGetInt32("zichtbaar");
            int ID_PT = dataReader.SafeGetInt32("ID_PT");
            ProductType productType = new ProductType { ID_PT = ID_PT };
            Product product = new Product { ID_P = productId, naam = productNaam, voorraad = voorraad, zichtbaar = zichtbaar, productType = productType };

            return product;
        }



        public Product GetProduct(string ProductID)
        {
            Product Product = null;
            try
            {
                conn.Open();

                string selectQueryproduct = @"SELECT * FROM product WHERE ID_P = @ID_P";
                MySqlCommand cmd = new MySqlCommand(selectQueryproduct, conn);

                MySqlParameter productidParam = new MySqlParameter("@ID_P", MySqlDbType.Int32);
                productidParam.Value = ProductID;
                cmd.Parameters.Add(productidParam);
                cmd.Prepare();

                MySqlDataReader dataReader = cmd.ExecuteReader();

                if (dataReader.Read())
                {
                    Product = GetproductFromDataReader(dataReader);
                }

            }
            catch (MySqlException e)
            {
                Console.Write("product niet opgehaald: " + e);
                throw e;
            }
            finally
            {
                conn.Close();
            }

            return Product;
        }


        protected Aanbieding GetAanbiedingFromDataReader(MySqlDataReader dataReader)
        {

            int aanbieding_a = dataReader.GetInt32("ID_A");
            string soort = dataReader.GetString("soort");
            int percentage = dataReader.GetInt32("percentage");
            bool actief = dataReader.GetBoolean("actief");
            Aanbieding aanbieding = new Aanbieding { ID_A = aanbieding_a, soort = soort, percentage = percentage, actief = actief };

            return aanbieding;
        }

        public Aanbieding GetAanbieding(int AanbiedingID)
        {
            bool opened = conn.State == System.Data.ConnectionState.Open || conn.State == System.Data.ConnectionState.Executing || conn.State == System.Data.ConnectionState.Fetching;
            if (AanbiedingID == 0)
            {
                return new Aanbieding();
            }
            Aanbieding Aanbieding = new Aanbieding();
            try
            {
                if (!opened)
                {
                    conn.Open();
                }

                string selectQueryproduct = @"SELECT * FROM aanbieding WHERE ID_A = @ID_A";
                MySqlCommand cmd = new MySqlCommand(selectQueryproduct, conn);

                MySqlParameter productidParam = new MySqlParameter("@ID_A", MySqlDbType.Int32);
                productidParam.Value = AanbiedingID;
                cmd.Parameters.Add(productidParam);
                cmd.Prepare();

                MySqlDataReader dataReader = cmd.ExecuteReader();

                if (dataReader.Read())
                {
                    Aanbieding = GetAanbiedingFromDataReader(dataReader);
                }

            }
            catch (MySqlException e)
            {
                Console.Write("aanbieding niet opgehaald: " + e);
                throw e;
            }
            finally
            {
                if (!opened)
                {
                    conn.Close();
                }
            }

            return Aanbieding;
        }

        public ProductType GetProductType(int PTID)
        {
            bool opened = conn.State == System.Data.ConnectionState.Open || conn.State == System.Data.ConnectionState.Executing || conn.State == System.Data.ConnectionState.Fetching;
            if (PTID == 0)
            {
                return new ProductType();
            }
            ProductType productType = new ProductType();
            try
            {
                if (!opened)
                {
                    conn.Open();
                }

                string selectQueryproduct = @"SELECT * FROM product_type WHERE ID_PT = @ID_PT";
                MySqlCommand cmd = new MySqlCommand(selectQueryproduct, conn);

                MySqlParameter productidParam = new MySqlParameter("@ID_PT", MySqlDbType.Int32);
                productidParam.Value = PTID;
                cmd.Parameters.Add(productidParam);
                cmd.Prepare();

                MySqlDataReader dataReader = cmd.ExecuteReader();

                if (dataReader.Read())
                {
                    productType = GetProductTypeFromDataReader(dataReader);
                }

            }
            catch (MySqlException e)
            {
                Console.Write("aanbieding niet opgehaald: " + e);
                throw e;
            }
            finally
            {
                if (!opened)
                {
                    conn.Close();
                }
            }

            return productType;
        }


        public Product GetProduct(int PID)
        {
            bool opened = conn.State == System.Data.ConnectionState.Open || conn.State == System.Data.ConnectionState.Executing || conn.State == System.Data.ConnectionState.Fetching;
            if (PID == 0)
            {
                return new Product();
            }
            Product Product = new Product();
            try
            {
                if (!opened)
                {
                    conn.Open();
                }

                string selectQueryproduct = @"SELECT * FROM product WHERE ID_P = @ID_P";
                MySqlCommand cmd = new MySqlCommand(selectQueryproduct, conn);

                MySqlParameter productidParam = new MySqlParameter("@ID_P", MySqlDbType.Int32);
                productidParam.Value = PID;
                cmd.Parameters.Add(productidParam);
                cmd.Prepare();

                MySqlDataReader dataReader = cmd.ExecuteReader();

                if (dataReader.Read())
                {
                    Product = GetproductFromDataReader(dataReader);
                }

            }
            catch (MySqlException e)
            {
                Console.Write("product niet opgehaald: " + e);
                throw e;
            }
            finally
            {
                if (!opened)
                {
                    conn.Close();
                }
            }

            return Product;
        }



        public void UpdateProduct(Product product)
        {

            MySqlTransaction trans = null;
            try
            {
                conn.Open();
                trans = conn.BeginTransaction();
                string insertString = @"update product set naam=@naam, voorraad=@voorraad,zichtbaar=@zichtbaar, ID_PT=@ID_PT  where ID_P=@ID_P";

                MySqlCommand cmd = new MySqlCommand(insertString, conn);
                MySqlParameter productnaamParam = new MySqlParameter("@naam", MySqlDbType.VarChar);
                MySqlParameter voorraadParam = new MySqlParameter("@voorraad", MySqlDbType.Int32);
                MySqlParameter zichtbaarParam = new MySqlParameter("@zichtbaar", MySqlDbType.Int32);
                MySqlParameter ID_PTParam = new MySqlParameter("@ID_PT", MySqlDbType.Int32);
                MySqlParameter idParam = new MySqlParameter("@ID_P", MySqlDbType.Int32);

                productnaamParam.Value = product.naam;
                voorraadParam.Value = product.voorraad;
                zichtbaarParam.Value = product.zichtbaar;
                ID_PTParam.Value = product.productType.ID_PT;
                idParam.Value = product.ID_P;

                cmd.Parameters.Add(productnaamParam);
                cmd.Parameters.Add(voorraadParam);
                cmd.Parameters.Add(zichtbaarParam);
                cmd.Parameters.Add(ID_PTParam);
                cmd.Parameters.Add(idParam);

                cmd.Prepare();
                cmd.ExecuteNonQuery();
                trans.Commit();

            }
            catch (MySqlException e)
            {
                trans.Rollback();
                Console.Write("Product niet upgedate: " + e);
                throw e;
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

                String insertString = @"INSERT INTO product (naam, voorraad, zichtbaar, ID_PT) values (@naam, @voorraad, @zichtbaar, @ID_PT)";

                MySqlCommand cmd = new MySqlCommand(insertString, conn);
                MySqlParameter naamParam = new MySqlParameter("@naam", MySqlDbType.VarChar);
                MySqlParameter voorraadParam = new MySqlParameter("@voorraad", MySqlDbType.Int32);
                MySqlParameter zichtbaarParam = new MySqlParameter("@zichtbaar", MySqlDbType.Int32);
                MySqlParameter ID_PTParam = new MySqlParameter("@ID_PT", MySqlDbType.Int32);

                naamParam.Value = product.naam;
                voorraadParam.Value = product.voorraad;
                zichtbaarParam.Value = product.zichtbaar;
                ID_PTParam.Value = product.productType.ID_PT;

                cmd.Parameters.Add(naamParam);
                cmd.Parameters.Add(voorraadParam);
                cmd.Parameters.Add(zichtbaarParam);
                cmd.Parameters.Add(ID_PTParam);

                cmd.Prepare();

                cmd.ExecuteNonQuery();

                trans.Commit();
            }
            catch (MySqlException e)
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
                                                    (pt.verkoop_prijs*sum(br.aantal)) as BRUTO_omzet, 
                                                    ((pt.verkoop_prijs-pt.inkoop_prijs)*sum(br.aantal)) as NETTO_omzet
                                                    FROM product_type pt left join product p on pt.ID_PT = p.ID_PT
                                                    left join bestel_regel br on p.ID_P = br.ID_P
													left join bestelling b on br.ID_B = b.ID_B
                                                    where  b.datum between @firstDate and @secondDate and b.status like '%betaald%'
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
            catch (MySqlException e)
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
                                                    (pt.verkoop_prijs*sum(br.aantal)) as BRUTO_omzet, 
                                                    ((pt.verkoop_prijs-pt.inkoop_prijs)*sum(br.aantal)) as NETTO_omzet
                                                    FROM product_type pt left join product p on pt.ID_PT = p.ID_PT
                                                    left join bestel_regel br on p.ID_P = br.ID_P
													left join bestelling b on br.ID_B = b.ID_B
                                                    where  b.datum between @firstDate and @secondDate and b.status like '%betaald%'
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
            catch (MySqlException e)
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

                string selectQuery = @"SELECT pt.ID_PT as Product_ID, pt.Naam as Naam, sum(br.aantal) as Afzet, pt.verkoop_prijs as Prijs
                                        FROM product_type pt left join product p on pt.ID_PT = p.ID_PT
                                                             left join bestel_regel br on p.ID_P = br.ID_P
                                                             left join bestelling b on br.ID_B = b.ID_B
                                        where b.status like '%betaald%'
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
            catch (MySqlException e)
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

                string selectQuery = @"SELECT pt.ID_PT as Product_ID, pt.Naam as Naam, sum(br.aantal) as Afzet, pt.verkoop_prijs as Prijs
                                        FROM product_type pt left join product p on pt.ID_PT = p.ID_PT
                                                             left join bestel_regel br on p.ID_P = br.ID_P
															left join bestelling b on br.ID_B = b.ID_B 
										WHERE b.datum between @firstDate and @secondDate and b.status like '%betaald%'
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
            catch (MySqlException e)
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

                string selectQuery = @"SELECT pt.ID_PT as Product_ID, pt.Naam as Naam, sum(br.aantal) as Afzet, pt.verkoop_prijs as Prijs
                                        FROM product_type pt left join product p on pt.ID_PT = p.ID_PT
                                                             left join bestel_regel br on p.ID_P = br.ID_P
															left join bestelling b on br.ID_B = b.ID_B 	
                                        where b.status like '%betaald%'									
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
            catch (MySqlException e)
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

                string selectQuery = @"SELECT pt.ID_PT as Product_ID, pt.Naam as Naam, sum(br.aantal) as Afzet, pt.verkoop_prijs as Prijs
                                        FROM product_type pt left join product p on pt.ID_PT = p.ID_PT
                                                             left join bestel_regel br on p.ID_P = br.ID_P
															left join bestelling b on br.ID_B = b.ID_B 
										WHERE b.datum between @firstDate and @secondDate and b.status like '%betaald%'
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
            catch (MySqlException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                conn.Close();
            }
            return producten;
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
            string maat = "";
            string image_path = "";
            float verkoopprijs = 0;
            string omschrijving = "";
            string merk = "";


            try
            {
                conn.Open();

                string selectQuery = @"select p.ID_p as ID_P, p.naam as naam, p.voorraad as voorraad, p.zichtbaar as zichtbaar, 
                                              pt.ID_PT as ID_PT, pt.naam as naam_producttype, pt.image_path as image_path,pt.omschrijving as omschrijving ,pt.verkoop_prijs as verkoop_prijs,pt.merk as merk,  e.naam as naame
                                       from product p
                                       left join product_type pt on p.ID_PT = pt.ID_PT
										left join eigenschap_product ep on p.ID_P = ep.ID_P
                                       left join eigenschap e on ep.ID_E = E.ID_E
                                       group by p.ID_P;";
                MySqlCommand cmd = new MySqlCommand(selectQuery, conn);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    ID_P = dataReader.SafeGetInt32("ID_P");
                    naam = dataReader.SafeGetString("naam");
                    omschrijving = dataReader.SafeGetString("omschrijving");
                    merk = dataReader.SafeGetString("merk");
                    voorraad = dataReader.SafeGetInt32("voorraad");
                    zichtbaar = dataReader.SafeGetInt32("zichtbaar");
                    ID_PT = dataReader.SafeGetInt32("ID_PT");
                    image_path = dataReader.SafeGetString("image_path");
                    verkoopprijs = dataReader.SafeGetInt32("verkoop_prijs");
                    naamPT = dataReader.SafeGetString("naam_producttype");
                    maat = dataReader.SafeGetString("naame");

                    ProductType productType = new ProductType { ID_PT = ID_PT, Naam = naamPT, ImagePath = image_path, VerkoopPrijs = verkoopprijs, Omschrijving = omschrijving, Merk = merk };
                    Product product = new Product { ID_P = ID_P, naam = naam, voorraad = voorraad, zichtbaar = zichtbaar, productType = productType, Maat = maat };
                    productenLijst.Add(product);
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Ophalen van typeProduct mislukt" + e);
            }
            finally
            {
                conn.Close();
            }
            return productenLijst;
        }

        public List<BestelRegel> GetBestellingOverzicht()
        {
            List<BestelRegel> bestellingenLijst = new List<BestelRegel>();

            int ID_P = 0;
            int ID_B = 0;
            string productNaam = "";
            int aantal = 0;
            double bedrag = 0;
            string status = "";
            string datum = "";

            try
            {
                conn.Open();

                string selectQuery = @"select br.ID_P as productID, br.ID_B as bestellingID, br.aantal as aantal, br.bedrag as bedrag, p.naam as productNaam, b.status as status, b.datum as datum from bestel_regel br left join bestelling b on br.ID_B = b.ID_B left join klant k on b.ID_K = k.ID_G left join product p on br.ID_P = p.ID_P where k.ID_G = 1 group by p.ID_P;";
                MySqlCommand cmd = new MySqlCommand(selectQuery, conn);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    ID_P = dataReader.GetInt32("productID");
                    ID_B = dataReader.GetInt32("bestellingID");
                    productNaam = dataReader.GetString("productNaam");
                    aantal = dataReader.GetInt32("aantal");
                    bedrag = dataReader.GetDouble("bedrag");
                    status = dataReader.GetString("status");
                    datum = dataReader.GetString("datum");



                    Bestelling bestelling = new Bestelling { status = status, datum = datum };
                    Product product = new Product { naam = productNaam };
                    BestelRegel bestelRegel = new BestelRegel { ID_B = ID_B, ID_P = ID_P, bedrag = bedrag, aantal = aantal, bestelling = bestelling, product = product };

                    bestellingenLijst.Add(bestelRegel);
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Ophalen van bestelregels mislukt" + e);
            }
            finally
            {
                conn.Close();
            }
            return bestellingenLijst;
        }

        public bool ControleerGoldMember()
        {

            bool gold = false;
            double totaalAankoop = 0;



            try
            {
                conn.Open();

                string selectQuery = @"SELECT sum(br.bedrag)
                                    FROM bestel_regel br left join bestelling b on br.ID_B = b.ID_B
                                    left join klant k on b.ID_K = k.ID_G
                                    WHERE k.ID_G = 1;";
                MySqlCommand cmd = new MySqlCommand(selectQuery, conn);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    totaalAankoop = dataReader.GetDouble("sum(br.bedrag)");


                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Ophalen van bestelregels mislukt" + e);
            }
            finally
            {
                conn.Close();
            }

            //totaalAankoop = 500.01;

            if (totaalAankoop >= 500)
            {
                gold = true;
            }
            else
            {
                gold = false;
            }

            return gold;
        }

        public List<ProductType> GetTypeLijst()
        {
            List<ProductType> productenType = new List<ProductType>();
            int ID_PT;
            string naamProduct;
            String omschrijving;
            String imagePath;
            bool zichtbaar;
            float inkoopPrijs;
            float verkoopPrijs;
            String merk;
            int ID_A;
            try
            {
                conn.Open();

                string selectQuery = @"SELECT * FROM product_type";

                MySqlCommand cmd = new MySqlCommand(selectQuery, conn);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    ID_PT = dataReader.SafeGetInt16("ID_PT");
                    naamProduct = dataReader.SafeGetString("naam");
                    inkoopPrijs = dataReader.SafeGetFloat("inkoop_prijs");
                    verkoopPrijs = dataReader.SafeGetFloat("verkoop_prijs");
                    omschrijving = dataReader.SafeGetString("omschrijving");
                    imagePath = dataReader.SafeGetString("image_path");
                    zichtbaar = dataReader.SafeGetBoolean("zichtbaar");
                    ID_A = dataReader.SafeGetInt32("ID_A");
                    merk = dataReader.SafeGetString("merk");
                    ProductType productType = new ProductType { ID_PT = ID_PT, Naam = naamProduct, InkoopPrijs = inkoopPrijs, VerkoopPrijs = verkoopPrijs, Omschrijving = omschrijving, ImagePath = imagePath, ID_A = ID_A, Zichtbaar = zichtbaar, Merk = merk };
                    productenType.Add(productType);
                }

                dataReader.Close();
                Console.WriteLine(productenType);
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Ophalen van typeProduct mislukt" + e);
            }
            finally
            {
                conn.Close();
            }
            for (int i = 0; i < productenType.Count; i++)
            {
                productenType[i].Aanbieding = GetAanbieding(productenType[i].ID_A);
            }
            return productenType;
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
            catch (MySqlException e)
            {
                Console.Write("Gebruiker niet toegevoegd: " + e);
            }
            finally
            {
                conn.Close();
            }
        }

        public void verwijderProduct(int ProductId)
        {
            Console.WriteLine(ProductId);
            MySqlTransaction trans = null;
            try
            {
                conn.Open();
                trans = conn.BeginTransaction();

                String DeleteProductTypeString = @"DELETE FROM product WHERE ID_P = @productID";

                MySqlCommand cmd = new MySqlCommand(DeleteProductTypeString, conn);
                MySqlParameter IdParam = new MySqlParameter("@productID", MySqlDbType.Int32);

                IdParam.Value = ProductId;

                cmd.Parameters.Add(IdParam);

                cmd.Prepare();

                cmd.ExecuteNonQuery();

                trans.Commit();
            }
            catch (MySqlException e)
            {
                trans.Rollback();
                Console.Write("Gebruiker niet toegevoegd: " + e);
            }
            finally
            {
                conn.Close();
            }
        }

        public void UpdateProductType(ProductType productType)
        {
            MySqlTransaction trans = null;
            try
            {
                conn.Open();
                trans = conn.BeginTransaction();
                string insertString = @"update product_type set naam = @naam, inkoop_prijs = @inkoop_prijs, verkoop_prijs = @verkoop_prijs, omschrijving = @omschrijving, image_path = @image_path, zichtbaar = @zichtbaar, ID_A = @ID_A, merk = @merk where ID_PT = @ID_PT";

                MySqlCommand cmd = new MySqlCommand(insertString, conn);
                MySqlParameter productTypeNaamParam = new MySqlParameter("@naam", MySqlDbType.VarChar);
                MySqlParameter inkoopPrijsParam = new MySqlParameter("@inkoop_prijs", MySqlDbType.Float);
                MySqlParameter verkoopPrijsParam = new MySqlParameter("@verkoop_prijs", MySqlDbType.Float);
                MySqlParameter omschrijvingParam = new MySqlParameter("@omschrijving", MySqlDbType.VarChar);
                MySqlParameter image_pathNaamParam = new MySqlParameter("@image_path", MySqlDbType.VarChar);
                MySqlParameter zichbaarParam = new MySqlParameter("@zichtbaar", MySqlDbType.Int16);
                MySqlParameter aanbiedingParam = new MySqlParameter("@ID_A", MySqlDbType.Int32);
                MySqlParameter merkParam = new MySqlParameter("@merk", MySqlDbType.VarChar);
                MySqlParameter idParam = new MySqlParameter("@ID_PT", MySqlDbType.Int16);

                productTypeNaamParam.Value = productType.Naam;
                inkoopPrijsParam.Value = productType.InkoopPrijs;
                verkoopPrijsParam.Value = productType.VerkoopPrijs;
                omschrijvingParam.Value = productType.Omschrijving;
                image_pathNaamParam.Value = productType.ImagePath;
                zichbaarParam.Value = productType.Zichtbaar;
                aanbiedingParam.Value = productType.Aanbieding.ID_A;
                merkParam.Value = productType.Merk;

                idParam.Value = productType.ID_PT;

                cmd.Parameters.Add(productTypeNaamParam);
                cmd.Parameters.Add(inkoopPrijsParam);
                cmd.Parameters.Add(verkoopPrijsParam);
                cmd.Parameters.Add(omschrijvingParam);
                cmd.Parameters.Add(image_pathNaamParam);
                cmd.Parameters.Add(zichbaarParam);
                cmd.Parameters.Add(aanbiedingParam);
                cmd.Parameters.Add(merkParam);
                cmd.Parameters.Add(idParam);

                cmd.Prepare();
                cmd.ExecuteNonQuery();
                trans.Commit();

            }
            catch (MySqlException e)
            {
                trans.Rollback();
                Console.Write("Genre niet upgedate: " + e);
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }
        protected ProductType GetProductTypeFromDataReader(MySqlDataReader dataReader)
        {
            int ID_PT;
            string naamProduct;
            String omschrijving;
            String imagePath;
            bool zichtbaar;
            int aanbieding;
            float inkoopPrijs;
            float verkoopPrijs;
            String merk;

            ID_PT = dataReader.GetInt16("ID_PT");
            naamProduct = dataReader.GetString("naam");
            inkoopPrijs = dataReader.GetFloat("inkoop_prijs");
            verkoopPrijs = dataReader.GetFloat("verkoop_prijs");
            omschrijving = dataReader.GetString("omschrijving");
            imagePath = dataReader.GetString("image_path");
            zichtbaar = dataReader.GetBoolean("zichtbaar");
            aanbieding = dataReader.GetInt32("ID_A");
            merk = dataReader.GetString("merk");


            ProductType productType = new ProductType { ID_PT = ID_PT, Naam = naamProduct, InkoopPrijs = inkoopPrijs, VerkoopPrijs = verkoopPrijs, Omschrijving = omschrijving, ImagePath = imagePath, ID_A = aanbieding, Zichtbaar = zichtbaar, Merk = merk };
            // product.Add(product);

            return productType;
        }

        public ProductType GetProductType(string ProductTypeID)
        {
            ProductType ProductType = null;
            try
            {
                conn.Open();

                string selectQueryproduct = @"SELECT * FROM product_type WHERE ID_PT = @ID_PT";
                MySqlCommand cmd = new MySqlCommand(selectQueryproduct, conn);

                MySqlParameter productTypeidParam = new MySqlParameter("@ID_PT", MySqlDbType.Int32);
                productTypeidParam.Value = ProductTypeID;
                cmd.Parameters.Add(productTypeidParam);
                cmd.Prepare();

                MySqlDataReader dataReader = cmd.ExecuteReader();

                if (dataReader.Read())
                {
                    ProductType = GetProductTypeFromDataReader(dataReader);
                }

            }
            catch (MySqlException e)
            {
                Console.Write("product Type niet opgehaald: " + e);
                throw e;
            }
            finally
            {
                conn.Close();
            }
            ProductType.Aanbieding = GetAanbieding(ProductType.ID_A);
            return ProductType;
        }

        public List<ProductType> GetAllProductTypes()
        {
            List<ProductType> productTypes = new List<ProductType>();
            int productTypeId = 0;
            string productName = "";

            try
            {
                conn.Open();

                string selectQueryOmzetMonthly = @"SELECT ID_PT as ID_ProductType, naam as Naam FROM product_type";
                MySqlCommand cmd = new MySqlCommand(selectQueryOmzetMonthly, conn);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    productTypeId = dataReader.GetInt32("ID_ProductType");
                    productName = dataReader.GetString("Naam");

                    ProductType product = new ProductType { Naam = productName, ID_PT = productTypeId };

                    productTypes.Add(product);
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
            return productTypes;
        }

        public Aanbieding GetAanbieding(string aanbiedingID)
        {
            Aanbieding aanbieding = null;
            try
            {
                conn.Open();

                string selectQueryaanbieding = @"SELECT * FROM aanbieding WHERE ID_A = @ID_A";
                MySqlCommand cmd = new MySqlCommand(selectQueryaanbieding, conn);

                MySqlParameter aanbiedingIDParam = new MySqlParameter("@ID_A", MySqlDbType.Int32);
                aanbiedingIDParam.Value = aanbiedingID;
                cmd.Parameters.Add(aanbiedingIDParam);
                cmd.Prepare();

                MySqlDataReader dataReader = cmd.ExecuteReader();

                if (dataReader.Read())
                {
                    aanbieding = GetAanbiedingFromDataReader(dataReader);
                }

            }
            catch (MySqlException e)
            {
                Console.Write("aanbieding Type niet opgehaald: " + e);
                throw e;
            }
            finally
            {
                conn.Close();
            }

            return aanbieding;
        }

        public List<Aanbieding> GetAllAanbieding()
        {
            List<Aanbieding> aanbiedingen = new List<Aanbieding>();
            int aanbieding_ID;
            string soort;
            int percentage;
            bool actief;

            try
            {
                conn.Open();

                string selectAanbieding = @"SELECT ID_A as ID_Aanbieding, soort as Soort, percentage as Percentage, actief as Actief  FROM aanbieding";
                MySqlCommand cmd = new MySqlCommand(selectAanbieding, conn);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    aanbieding_ID = dataReader.GetInt32("ID_A");
                    soort = dataReader.GetString("soort");
                    percentage = dataReader.GetInt32("percentage");
                    actief = dataReader.GetBoolean("actie");

                    Aanbieding aanbieding = new Aanbieding { ID_A = aanbieding_ID, soort = soort, percentage = percentage, actief = actief };

                    aanbiedingen.Add(aanbieding);
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
            return aanbiedingen;
        }


        public void InsertAanbieding(Aanbieding aanbieding)
        {

            MySqlTransaction trans = null;
            try
            {
                conn.Open();
                trans = conn.BeginTransaction();

                String insertString = @"INSERT INTO aanbieding (soort, percentage, actief)
                                        VALUES (@soort, @percentage, @actief)";

                MySqlCommand cmd = new MySqlCommand(insertString, conn);
                MySqlParameter soortParam = new MySqlParameter("@soort", MySqlDbType.VarChar);
                MySqlParameter percentageParam = new MySqlParameter("@percentage", MySqlDbType.Int32);
                MySqlParameter actiefParam = new MySqlParameter("@actief", MySqlDbType.Int32);


                soortParam.Value = aanbieding.soort;
                percentageParam.Value = aanbieding.percentage;
                actiefParam.Value = aanbieding.actief;

                cmd.Parameters.Add(soortParam);
                cmd.Parameters.Add(percentageParam);
                cmd.Parameters.Add(actiefParam);
                cmd.Prepare();

                cmd.ExecuteNonQuery();

                trans.Commit();
            }
            catch (MySqlException e)
            {
                trans.Rollback();
                Console.Write("aanbieding is niet toegevoegd: " + e);
            }
            finally
            {
                conn.Close();
            }
        }

        // AanbiedingDBController

        public Aanbieding GetAAnbieding(int aanbiedingID)
        {
            Aanbieding aanbieding = null;
            try
            {
                conn.Open();

                string selectQueryaanbieding = @"SELECT * FROM aanbieding WHERE ID_A = @ID_A";
                MySqlCommand cmd = new MySqlCommand(selectQueryaanbieding, conn);

                MySqlParameter aanbiedingIDParam = new MySqlParameter("@ID_A", MySqlDbType.Int32);
                aanbiedingIDParam.Value = aanbiedingID;
                cmd.Parameters.Add(aanbiedingIDParam);
                cmd.Prepare();

                MySqlDataReader dataReader = cmd.ExecuteReader();

                if (dataReader.Read())
                {
                    aanbieding = GetAanbiedingFromDataReader(dataReader);
                }

            }
            catch (MySqlException e)
            {
                Console.Write("aanbieding Type niet opgehaald: " + e);
                throw e;
            }
            finally
            {
                conn.Close();
            }

            return aanbieding;
        }

        public void UpdateAanbieding(Aanbieding aanbieding)
        {

            MySqlTransaction trans = null;
            try
            {
                conn.Open();
                trans = conn.BeginTransaction();
                string insertString = @"Update aanbieding SET soort=@soort, percentage=@percentage, actief=@actief where ID_A=@ID_A";

                MySqlCommand cmd = new MySqlCommand(insertString, conn);
                MySqlParameter soortParam = new MySqlParameter("@soort", MySqlDbType.VarChar);
                MySqlParameter percentageParam = new MySqlParameter("@percentage", MySqlDbType.Int32);
                MySqlParameter actiefParam = new MySqlParameter("@actief", MySqlDbType.Int32);
                MySqlParameter ID_AParam = new MySqlParameter("@ID_A", MySqlDbType.Int32);

                soortParam.Value = aanbieding.soort;
                percentageParam.Value = aanbieding.percentage;
                actiefParam.Value = aanbieding.actief;
                ID_AParam.Value = aanbieding.ID_A;

                cmd.Parameters.Add(soortParam);
                cmd.Parameters.Add(percentageParam);
                cmd.Parameters.Add(actiefParam);
                cmd.Parameters.Add(ID_AParam);

                cmd.Prepare();
                cmd.ExecuteNonQuery();
                trans.Commit();

            }
            catch (MySqlException e)
            {
                trans.Rollback();
                Console.Write("Aanbieding niet upgedate: " + e);
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }

        public void DeleteAanbieding(int aanbiedingId)
        {
            MySqlTransaction trans = null;
            try
            {
                conn.Open();
                trans = conn.BeginTransaction();

                String DeleteAanbiedingString = @"DELETE FROM aanbieding WHERE ID_A = @ID_A";

                MySqlCommand cmd = new MySqlCommand(DeleteAanbiedingString, conn);
                MySqlParameter IdParam = new MySqlParameter("@ID_A", MySqlDbType.Int32);

                IdParam.Value = aanbiedingId;

                cmd.Parameters.Add(IdParam);

                cmd.Prepare();

                cmd.ExecuteNonQuery();

                trans.Commit();
            }
            catch (MySqlException e)
            {
                trans.Rollback();
                Console.Write("Aanbieding is niet verwijdert: " + e);
            }
            finally
            {
                conn.Close();
            }
        }

        public void DeleteAanbieding(Aanbieding aanbieding)
        {
            MySqlTransaction trans = null;
            try
            {
                conn.Open();
                trans = conn.BeginTransaction();

                String DeleteAanbiedingString = @"DELETE FROM aanbieding WHERE ID_A = @ID_A";

                MySqlCommand cmd = new MySqlCommand(DeleteAanbiedingString, conn);
                MySqlParameter IdParam = new MySqlParameter("@ID_A", MySqlDbType.Int32);

                IdParam.Value = aanbieding.ID_A;

                cmd.Parameters.Add(IdParam);

                cmd.Prepare();

                cmd.ExecuteNonQuery();

                trans.Commit();
            }
            catch (MySqlException e)
            {
                trans.Rollback();
                Console.Write("Aanbieding is niet verwijdert: " + e);
            }
            finally
            {
                conn.Close();
            }
        }

        public List<Aanbieding> GetAanbiedingen1()
        {
            List<Aanbieding> aanbiedingen = new List<Aanbieding>();
            try
            {
                conn.Open();

                string selectQuery = @"SELECT * FROM aanbieding";

                MySqlCommand cmd = new MySqlCommand(selectQuery, conn);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    Aanbieding aanbieding = GetAanbiedingFromDataReader(dataReader);
                    aanbiedingen.Add(aanbieding);
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Ophalen van aanbiedingen mislukt" + e);

            }
            finally
            {
                conn.Close();
            }
            return aanbiedingen;
        }

        public List<BestelRegel> GetAllOrderedProducts()
        {
            List<BestelRegel> BesteldeProducten = new List<BestelRegel>();
            int productID = 0;
            string naamProduct = "";
            int aantalProducten = 0;
            double bedragBestelling = 0;
            DateTime datumBestelling = DateTime.Now;

            try
            {
                conn.Open();

                string selectQuery = "SELECT p.ID_P as Product_ID, p.naam as Naam, br.aantal as Aantal, br.bedrag as Bedrag, b.datum as Datum FROM product p JOIN bestel_regel br ON p.ID_P = br.ID_P JOIN bestelling b ON b.ID_B = br.ID_B WHERE br.aantal IS NOT NULL GROUP BY p.ID_P;";

                MySqlCommand cmd = new MySqlCommand(selectQuery, conn);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                    {
                        productID = dataReader.GetInt32("Product_ID");
                        naamProduct = dataReader.GetString("Naam");
                        aantalProducten = dataReader.GetInt32("Aantal");
                        bedragBestelling = dataReader.GetDouble("Bedrag");
                        datumBestelling = dataReader.GetDateTime("Datum");
                        BestelRegel BesteldProduct = new BestelRegel {ID_P = productID,  naam = naamProduct, aantal = aantalProducten, bedrag = bedragBestelling, datum = datumBestelling };

                        BesteldeProducten.Add(BesteldProduct);
                    }
                    
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Ophalen van bestelde producten niet gelukt" + e);
            }
            finally
            {
                conn.Close();
            }
            return BesteldeProducten;
        }

       

        public void InsertBestelling()
        {

            int ID_K = 1;
            //getID_K uit sessie
            string status = "besteld";
            string datum = DateTime.Now.ToString("yyyy-MM-dd");


            MySqlTransaction trans = null;
            try
            {
                conn.Open();
                trans = conn.BeginTransaction();
             

                String insertString = @"INSERT INTO bestelling (ID_K, status, datum) VALUES (@ID_K, @status, @datum)";

                MySqlCommand cmd = new MySqlCommand(insertString, conn);
                MySqlParameter ID_KParam = new MySqlParameter("@ID_K", MySqlDbType.Int32);
                MySqlParameter statusParam = new MySqlParameter("@status", MySqlDbType.VarChar);
                MySqlParameter datumParam = new MySqlParameter("@datum", MySqlDbType.VarChar);


                ID_KParam.Value = ID_K;
                statusParam.Value = status;
                datumParam.Value = datum;

                /* @TODO Korting toevoegen zodra een product getoond wordt aan de klant */



                cmd.Parameters.Add(ID_KParam);
                cmd.Parameters.Add(statusParam);
                cmd.Parameters.Add(datumParam);
               

                cmd.Prepare();

                cmd.ExecuteNonQuery();

                trans.Commit();
            }
            catch (MySqlException e)
            {
                trans.Rollback();
                Console.Write("Gebruiker niet toegevoegd: " + e);
            }
            finally
            {
                conn.Close();
            }

        }

        public int HaalBestelNummerUitDB() {
            int ID_B = 0;
            //int ID_K = get uit sessie;

            try
            {
           
                conn.Open();

                string selectQuery = @"SELECT b.ID_B as ID_B
FROM bestelling b 
where b.ID_K = 1
ORDER BY b.ID_B DESC
LIMIT 1;";


                    MySqlCommand cmd = new MySqlCommand(selectQuery, conn);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        ID_B = dataReader.GetInt32("ID_B");
                    }
                }
                catch (MySqlException e)
                {
                    Console.WriteLine("Ophalen van ID_B mislukt" + e);

                }
                finally
                {
                    conn.Close();
                }




            return ID_B;
        }

        public void InsertBestelRegel(int ID_B, int ID_P, int aantal, float bedrag) {

            //ID_P, ID_B(haal uit database), aantal, totaalbedrag
   
           // int ID_P = get uit view
            //int aantal = get uit view
           // float totaalbedrag = get uit view
           

            MySqlTransaction trans = null;
            try
            {
                conn.Open();
                trans = conn.BeginTransaction();


                String insertString = @"INSERT INTO bestel_regel (ID_P, ID_B, aantal, bedrag) VALUES (@ID_P, @ID_B, @aantal, @bedrag)";

                MySqlCommand cmd = new MySqlCommand(insertString, conn);
                MySqlParameter ID_PParam = new MySqlParameter("@ID_P", MySqlDbType.Int32);
                MySqlParameter ID_BParam = new MySqlParameter("@ID_B", MySqlDbType.Int32);
                MySqlParameter aantalParam = new MySqlParameter("@aantal", MySqlDbType.Int32);
                MySqlParameter bedragParam = new MySqlParameter("@bedrag", MySqlDbType.Float);



                ID_PParam.Value = ID_P;
                ID_BParam.Value = ID_B;
                aantalParam.Value = aantal;
                bedragParam.Value = bedrag;





                cmd.Parameters.Add(ID_PParam);
                cmd.Parameters.Add(ID_BParam);
                cmd.Parameters.Add(aantalParam);
                cmd.Parameters.Add(bedragParam);


                cmd.Prepare();

                cmd.ExecuteNonQuery();

                trans.Commit();
            }
            catch (MySqlException e)
            {
                trans.Rollback();
                Console.Write("Gebruiker niet toegevoegd: " + e);
            }
            finally
            {
                conn.Close();
            }

        }

        public void BestelProduct(int ID_P, int aantal, float bedrag)
        {
            int ID_B;
            InsertBestelling();

            ID_B = HaalBestelNummerUitDB();

            InsertBestelRegel(ID_B, ID_P, aantal, bedrag);
        }

        }

    

    }

