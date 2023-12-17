using Cheremushkinae_107d2.Model.Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Cheremushkinae_107d2.Model
{
    public static class DataWorker
    {
        // add user
        public static string CreateUserInDB(string username, string password, string email)
        {
            string result = "User with this nickname already exists";
            using (ApplicationContext db = new ApplicationContext())
            {
                // проверяем, существует ли пользователь
                bool checkIsExist = db.Users.Any(el => el.Username == username);
                if (!checkIsExist) 
                {
                    User newUser = new User { Username = username, Password = password, Email = email};
                    db.Users.Add(newUser);
                    db.SaveChanges();
                    // Получаем ID только что созданного пользователя
                    GlobalSettings.SavedUserID = newUser.ID_user;
                    GlobalSettings.SavedUsername = newUser.Username;
                    GlobalSettings.SavedLearnWordsCount = 0;
                    result = "You have successfully registered!";
                }
            }
            return result;
        }
        // sign in 
        public static string SignInDB(string username, string password)
        {
            string result = "User with this nickname does not exist or the password is entered incorrectly";
            using (ApplicationContext db = new ApplicationContext())
            {
                // проверяем, существует ли пользователь
                bool checkIsExist = db.Users.Any(el => el.Username == username);
                if (checkIsExist)
                {
                    User user = db.Users.FirstOrDefault(x => x.Username == username);
                    if (user.Password == password)
                    {
                        GlobalSettings.SavedUserID = user.ID_user;
                        GlobalSettings.SavedUsername = user.Username;
                        GlobalSettings.SavedLearnWordsCount = GetAllUserLearningWords(user.ID_user).Count;
                        result = "You have successfully sign in!";
                    } 
                }
            }
            return result;
        }
        // add new word
        public static string AddNewWordInDB(string word_in_english, string word_in_russian, string using_example)
        {
            string result = "This word has already been added";
            using (ApplicationContext db = new ApplicationContext())
            {
                User user = db.Users.FirstOrDefault(x => x.Username == GlobalSettings.SavedUsername);
                // проверяем, существует ли слово на английском
                bool checkIsExist = db.LearnDicts.Any(el => el.Word_in_English == word_in_english && el.ID_user == user.ID_user);
                if (!checkIsExist)
                {
                    LearnDict newLearnDict = new LearnDict {
                        ID_user = user.ID_user,
                        Word_in_English = word_in_english,
                        Word_in_Russian = word_in_russian,
                        Using_example = using_example,
                        Right_answers_count = 0 };
                    db.LearnDicts.Add(newLearnDict);
                    db.SaveChanges();
                    GlobalSettings.SavedLearnWordsCount += 1;
                    result = "You have successfully added a new word";
                }
            }
            return result;
        }
        // update word
        public static string UpdateLearningWordInDB(string word_in_english, string word_in_russian, string using_example, int right_ans_count)
        {
            string result = "Unexpected error";
            if (right_ans_count >= 5)
            {
                result = "Add in known";
                AddKnownWordInDB(word_in_english, word_in_russian, using_example);
            }
            using (ApplicationContext db = new ApplicationContext())
            {
                User user = db.Users.FirstOrDefault(x => x.Username == GlobalSettings.SavedUsername);
                bool checkIsExist = db.LearnDicts.Any(el => el.Word_in_English == word_in_english && el.ID_user == user.ID_user);
                if(checkIsExist)
                {
                    
                    LearnDict learnDict = db.LearnDicts.FirstOrDefault(x => x.Word_in_English == word_in_english && user.ID_user == GlobalSettings.SavedUserID);
                    learnDict.Word_in_English = word_in_english;
                    learnDict.Word_in_Russian = word_in_russian;
                    learnDict.Using_example = using_example;
                    learnDict.Right_answers_count = right_ans_count;
                    db.SaveChanges();
                    result = "You have successfully updated a word";
                } else
                {
                    result = "You didn’t add this word in english";
                }
                
            }
            return result;
        }
        // add word in known
        public static string AddKnownWordInDB(string word_in_english, string word_in_russian, string using_example)
        {
            string result = "Done";
            using (ApplicationContext db = new ApplicationContext())
            {
                User user = db.Users.FirstOrDefault(x => x.Username == GlobalSettings.SavedUsername);
                LearnDict learnDict = db.LearnDicts.FirstOrDefault(x => x.Word_in_English == word_in_english && user.ID_user == GlobalSettings.SavedUserID);
                KnowDict newKnowDict = new KnowDict
                {
                    ID_user = user.ID_user,
                    Word_in_English = word_in_english,
                    Word_in_Russian = word_in_russian,
                    Using_example = using_example
                };
                db.LearnDicts.Remove(learnDict);
                GlobalSettings.SavedLearnWordsCount -= 1;
                db.KnowDicts.Add(newKnowDict);
                db.SaveChanges();

            }
            return result;
        }
        // delete user
        public static string DeleteUserInDB(string username)
        {
            string result = "Такого пользователя не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                User user = db.Users.FirstOrDefault(x => x.Username == username);
                db.Users.Remove(user);
                db.SaveChanges();
                result = "Пользователь с ником " + user.Username + " удален";
                GlobalSettings.SavedUsername = null;
                GlobalSettings.SavedUserID = 0;
                GlobalSettings.SavedLearnWordsCount = 0;

            }
            return result;
        }
        // delete word in learning
        public static string DeleteLearningWordInDB(string word_in_eng)
        {
            string result = "Такого слова вы не добавляли";
            using (ApplicationContext db = new ApplicationContext())
            {
                User user = db.Users.FirstOrDefault(x => x.Username == GlobalSettings.SavedUsername);
                LearnDict learnDict = db.LearnDicts.FirstOrDefault(l => l.Word_in_English == word_in_eng);
                db.LearnDicts.Remove(learnDict);
                db.SaveChanges();
                result = "cлово " + learnDict.Word_in_English + " удалено";
 
            }
            GlobalSettings.SavedLearnWordsCount -= 1;
            return result;
        }
        // delete word in known
        public static string DeleteKnownWordInDB(KnowDict knowDict)
        {
            string result = "Такого слова в выученных не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                db.KnowDicts.Remove(knowDict);
                db.SaveChanges();
                result = "cлово " + knowDict.Word_in_English + " удалено";
   
            }
            return result;
        }

        // take all Learning words
        public static List<LearnDict> GetAllUserLearningWords(int userID)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.LearnDicts.Where(x => x.ID_user == userID).ToList();
                return result;
            }
            
        }
        // take all known words
        public static List<KnowDict> GetAllKnowWords()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.KnowDicts.ToList();
                return result;
            }

        }
    }
}
