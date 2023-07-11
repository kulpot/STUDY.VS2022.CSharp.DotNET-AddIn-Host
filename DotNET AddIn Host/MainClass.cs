using System;
using System.Reflection;
using System.Linq;

//ref link:https://www.youtube.com/watch?v=zF2pgECcUa4&list=PLRwVmtr-pp05brRDYXh-OTAIi-9kYcw3r&index=18
// Host Code
/*          CLI
 *          
 *  12/07/2023  05:10 am    <DIR>          .
    12/07/2023  05:10 am    <DIR>          ..
    12/07/2023  05:02 am    <DIR>          bin
    28/06/2023  11:44 pm             4,608 ChessInterface.dll
    12/07/2023  05:01 am               303 DotNET AddIn Host.csproj
    12/07/2023  05:07 am             1,111 MainClass.cs
    29/06/2023  09:40 pm             3,584 MyChessAlgorithm.dll
    12/07/2023  05:02 am    <DIR>          obj
    29/06/2023  09:44 pm             3,584 YourChessAlgorithm.dll
               5 File(s)         13,190 bytes
               4 Dir(s)  497,090,678,784 bytes free
 * C:\Users\sunny\source\repos\DotNET AddIn Host\DotNET AddIn Host>csc /r:ChessInterface.dll /out:MyChessHost.exe MainClass.cs
 * 
 */

class MainClass
{
    static void Main()
    {
        Assembly player1Assembly = Assembly.Load("MyChessAlgorithm");
        Assembly player2Assembly = Assembly.Load("YourChessAlgorithm");
       
        IChessGame player1 = CreatePlayerAlgorithmInstance(player1Assembly);
        IChessGame player2 = CreatePlayerAlgorithmInstance(player2Assembly);

        ChessMove myMove = player1.MakeMove(null);
        ChessMove yourMove = player2.MakeMove(null);
        Console.WriteLine(myMove.StartColumn);
        Console.WriteLine(yourMove.StartColumn);
    }

    private static IChessGame CreatePlayerAlgorithmInstance(Assembly player1)
    {
        Type playerAlgorithmType =
            player1.GetTypes()
            .Single(t => t.GetInterfaces()
                        .Any(it => it.Equals(typeof(IChessGame))));
        return Activator.CreateInstance(playerAlgorithmType) as IChessGame;
    }
}