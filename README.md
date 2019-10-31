# Ocph.DAL.SQLite

Welcome to the OcphDAL for Simple  SQLite Data Access Layer !
1. Install From Nuget
   
   https://www.nuget.org/packages/Ocph.DAL.SQLite/

3. Create Model 

    Example :

           namespace Models.Data
               {
                [TableName("Player")]
                public class Player
                 {
                   [PrimaryKey("Id")]
                   [DbColumn("Id")]
                   public int Id? { get; set; }

                  [DbColumn("Name")]
                  public string Name { get; set; }
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
                  var players = db.Players.Select();
                      //With Linq 
                      var players = from data in db.Players.Select()
                                     select data;
                  // with Clause Where
                   var roadWords = db.Players.Where(O=>O.Id==1);
                        //With Linq
                         var rootWors = from data in db.Players.Select() where data.Id equal 1
                                     select data;

                  // You Can Join Like Linq
                   var data = from a in db.TableA  
                              join b in db.TableB on a.Id equal b.Ida
                              select b;
               }

   2. Insert
                 
               using(var db = new OcphDbContext())
               {
                    var saved = db.Players.Insert(model);
                    var Id = db.RootWords.InsertWithGetLastId(model);
                 }

                //With Transaction
                   using(var db = new OcphDbContext())
               {
                     var trans= db.Connection.BeginTransaction();
                   try
                     {
                           var Id = db.Players.InsertWithGetLastId(model);
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
                    var isDeleted = db.Players.Delete(O=>O.Id==1);
                 
                 }

    
   4. Update


               using(var db = new OcphDbContext())
               {
                    var isUpdated = db.Players.Update(O=> new{O.Word},playerModel,O=>O.Id==playerModelId );
                 
                 }


***
