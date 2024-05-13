using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Koolitused.Model
{
    public class KoolitusRepository
    {
        SQLiteConnection database;
        public KoolitusRepository(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable<Kasutaja>();
            database.CreateTable<Koolitus>();
            database.CreateTable<Opetaja>();
            database.CreateTable<Roll>();
        }

        // Repository Kasutaja
        public IEnumerable<Kasutaja> GetKasutajad()
        {
            return database.Table<Kasutaja>().ToList();
        }
        public Kasutaja GetKasutaja(int id)
        {
            return database.Get<Kasutaja>(id);
        }
        public int DeleteKasutaja(int id)
        {
            return database.Delete<Kasutaja>(id);
        }
        public int SaveKasutaja(Kasutaja item)
        {
            if (item.Id != 0)
            {
                database.Update(item);
                return item.Id;
            }
            else
            {
                return database.Insert(item);
            }
        }

        // Repository Koolitus
        public IEnumerable<Koolitus> GetKoolitused()
        {
            return database.Table<Koolitus>().ToList();
        }
        public Koolitus GetKoolitus(int id)
        {
            return database.Get<Koolitus>(id);
        }
        public int DeleteKoolitus(int id)
        {
            return database.Delete<Koolitus>(id);
        }
        public int SaveKoolitus(Koolitus item)
        {
            if (item.Id != 0)
            {
                database.Update(item);
                return item.Id;
            }
            else
            {
                return database.Insert(item);
            }
        }

        // Repository Opetaja
        public IEnumerable<Opetaja> GetOpetajad()
        {
            return database.Table<Opetaja>().ToList();
        }
        public Opetaja GetOpetaja(int id)
        {
            return database.Get<Opetaja>(id);
        }
        public int DeleteOpetaja(int id)
        {
            return database.Delete<Opetaja>(id);
        }
        public int SaveOpetaja(Opetaja item)
        {
            if (item.Id != 0)
            {
                database.Update(item);
                return item.Id;
            }
            else
            {
                return database.Insert(item);
            }
        }

        // Repository Roll
        public IEnumerable<Roll> GetRollid()
        {
            return database.Table<Roll>().ToList();
        }
        public Roll GetRoll(int id)
        {
            return database.Get<Roll>(id);
        }
        public int DeleteRoll(int id)
        {
            return database.Delete<Roll>(id);
        }
        public int SaveRoll(Roll item)
        {
            if (item.Id != 0)
            {
                database.Update(item);
                return item.Id;
            }
            else
            {
                return database.Insert(item);
            }
        }
    }
}
