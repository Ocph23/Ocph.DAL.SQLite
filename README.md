# Ocph.DAL.SQLite

Welcome to the OcphDAL for Simple  Mysql Data Access Layer !
1. Install From Nuget
    https://www.nuget.org/packages/Ocph.DAL.SQLite/

3. Create Model 

    Example :

           namespace Models.Data
               {
                [TableName("tb_rootword")]
                public class RootWord
                 {
                   [PrimaryKey("Id")]
                   [DbColumn("id_ktdasar")]
                   public int Id? { get; set; }

                  [DbColumn("rootword")]
                  public string Word { get; set; }

                 [DbColumn("tipe_katadasar")]
                 public string TypeRootWord { get; set; }
                  }
            }
   
4. Create DbContext And Repository
   ***
              
          using Ocph.DAL;
          Ocph.DAL.Provider.SQLite;
          using Ocph.DAL.Repository;
          using System;
      

          namespace MainApp
          {
              public class OcphDbContext : SQLiteDbConnection
              {
                  public OcphDbContext()
                  {
                      if (File.Exists("dbgame.db"))
                      {
                          this.ConnectionString = "Data Source=dbgame.db";
                      }
                      else
                      {
                          this.ConnectionString = "Data Source=dbgame.db;Version=3;New=True;Compress=True;";
                          this.Open();

                          IDbCommand cmd = CreateCommand();

                          cmd.CommandText = "CREATE TABLE IF NOT EXISTS Player (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name 
                                            VARCHAR(100))";

                          IDataReader reader = cmd.ExecuteReader();

                          reader.Close();

                          cmd.CommandText = "CREATE TABLE IF NOT EXISTS Greedy (Id INTEGER PRIMARY KEY AUTOINCREMENT, Guid text, board 
                                            INTEGER, awal text, akhir text, waktu long, file text)";

                          reader = cmd.ExecuteReader();

                          reader.Close();
                      }
                  }

                  public IRepository<PlayerModel> Players { get { return new Repository<PlayerModel>(this); } }
              }

          }
***

5. Query
   1. Select


              using(var db = new OcphDbContext())
               {
                  // Get All Data
                  var rootWords = db.RootWords.Select();
                      //With Linq 
                      var rootWors = from data in db.RootWords.Select()
                                     select data;
                  // with Clause Where
                   var roadWords = db.RoadWords.Where(O=>O.TypeRootWord=='Noun');
                        //With Linq
                         var rootWors = from data in db.RootWords.Select() where data.TypeRootWord equal 'Noun'
                                     select data;

                  // You Can Join Like Linq
                   var data = from a in db.TableA  
                              join b in db.TableB on a.Id equal b.Ida
                              select b;
               }

   2. Insert
                 
               using(var db = new OcphDbContext())
               {
                    var saved = db.RootWords.Insert(rootWord);
                    var Id = db.RootWords.InsertWithGetLastId(rootWord);
                 }

                //With Transaction
                   using(var db = new OcphDbContext())
               {
                     var trans= db.Connection.BeginTransaction();
                   try
                     {
                           var Id = db.RootWords.InsertWithGetLastId(rootWord);
                           var item1= new Item();
                           item1.Id=Id;
                            var saved = db.Items.Insert(item1);
                            trans.Commit();
                      }catch(MySqlExecption ex)
                      {
                           trans.Rollback();   
                      }
                    
                 }
                  
   3. Delete


               using(var db = new OcphDbContext())
               {
                    var isDeleted = db.RootWords.Delete(O=>O.Id==1);
                 
                 }

    
   4. Update


               using(var db = new OcphDbContext())
               {
                    var isUpdated = db.RootWords.Update(O=> new{O.Word},rootWordToUpdate,O=>O.Id==rootWordToUpdate );
                 
                 }


***
