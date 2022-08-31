using System;
using Firebase.Database;

namespace AppCompras.Data.Network.Conexions
{
    public class Conexion
    {
        public static FirebaseClient firebase =
             new FirebaseClient(
                 "https://appcompras-70626-default-rtdb.firebaseio.com/"
                 );
    }
}

