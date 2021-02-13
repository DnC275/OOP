using System;
using System.Collections.Generic;
using Backups2.MyExceptions;

namespace Backups2
{
    class Program
    {
        static void Main(string[] args)
        {
            string str;
            Backup mainBackup = null;
            while (true)
            {
                try
                {
                    str = Console.ReadLine();
                    string[] arr = str.Split();
                    if (arr.Length == 0)
                        continue;
                    switch (arr[0])
                    {
                        case "create":
                            if (arr.Length == 1)
                                throw new CreateException();
                            switch (arr[1])
                            {
                                case "-b":
                                    if (arr.Length == 2)
                                    {
                                        mainBackup = new Backup();
                                        goto LoopContinue;
                                    }

                                    if (arr.Length == 3)
                                    {
                                        mainBackup = new Backup(arr[2]);
                                        goto LoopContinue;
                                    }

                                    string name = arr[2];
                                    int count;
                                    try
                                    {
                                        count = Int32.Parse(arr[3]);
                                    }
                                    catch (Exception ex)
                                    {
                                        throw new CreateBackupException();
                                    }

                                    string[] paths = new string[count];
                                    Array.Copy(arr, 4, paths, 0, count);
                                    mainBackup = new Backup(name, paths);
                                    break;
                                
                                case "-rp":
                                    if (mainBackup == null)
                                        throw new CreateRestorePointException();
                                    if (arr.Length > 3)
                                    {
                                        throw new CreateRestorePointException();
                                    }
                                    string pointName = arr.Length == 2 ? null : arr[2];
                                    mainBackup.CreateRestorePoint(pointName);
                                    break;
                                
                                case "-drp":
                                    if (mainBackup == null)
                                        throw new UncreatedBackupException();

                                    if (arr.Length > 3)
                                        throw new CreateDeltaRestorePointException();

                                    if (arr.Length == 2)
                                    {
                                        mainBackup.CreateDeltaRestorePoint();
                                        break;
                                    }

                                    string delPointName = arr[2];
                                    mainBackup.CreateDeltaRestorePoint(delPointName);
                                    break;
                                default:
                                    throw new CreateException();
                            }

                            break;

                        case "sl":
                            switch (arr[1])
                            {
                                case "-c":
                                    if (arr.Length != 3)
                                        throw new SetCountLimitException();
                                    int countLimit;
                                    try
                                    {
                                        countLimit = int.Parse(arr[2]);
                                    }
                                    catch (Exception ex)
                                    {
                                        throw new SetCountLimitException();
                                    }
                                    mainBackup.SetCountLimit(countLimit);
                                    break;
                                case "-s":
                                    if (arr.Length != 3)
                                        throw new SetSizeLimitException();
                                    long sizeLimit;
                                    try
                                    {
                                        sizeLimit = int.Parse(arr[2]);
                                    }
                                    catch (Exception ex)
                                    {
                                        throw new SetSizeLimitException();
                                    }
                                    mainBackup.SetSizeLimit(sizeLimit);
                                    break;
                                case "-d":
                                    if (arr.Length != 5)
                                        throw new SetDateLimitException();
                                    int year, month, day;
                                    try
                                    {
                                        year = int.Parse(arr[2]);
                                        month = int.Parse(arr[3]);
                                        day = int.Parse(arr[4]);
                                    }
                                    catch (Exception ex)
                                    {
                                        throw new SetDateLimitException();
                                    }
                                    mainBackup.SetDateLimit(year, month, day);
                                    break;
                                case "-r": 
                                    if (arr.Length != 2)
                                        throw new SetDateLimitException();
                                    mainBackup.ResetLimits();
                                    break;

                                default:
                                    throw new SetLimitException();
                            }
                            break;
                        
                        case "slm":
                            if (arr.Length != 2)
                                throw new SetLimitModeException();
                            switch (arr[1])
                            {
                                case "-so":
                                    mainBackup.SetLimitsMode(LimitsMode.SuitableForSomeone);
                                    break;
                                case "-eo":
                                    mainBackup.SetLimitsMode(LimitsMode.SuitableForEveryone);
                                    break;
                                default:
                                    throw new SetLimitModeException();
                            }
                            break;
                        
                        case "add":
                            if (arr.Length != 2)
                                throw new AddException();
                            string addFilePath = arr[1];
                            mainBackup.AddFile(addFilePath);
                            break;
                        
                        case "delete":
                            if (arr.Length != 2)
                                throw new DeleteException();
                            string delFilePath = arr[1];
                            mainBackup.DeleteFile(delFilePath);
                            break;
                        
                        case "show":
                            switch (arr[1])
                            {
                                case "-p":
                                    if (arr.Length != 2)
                                        throw new ShowException();
                                    if (mainBackup == null) 
                                        throw new UncreatedBackupException();
                                    List<string> names = mainBackup.GetNamesOfPoints();
                                    foreach (string pointName in names)
                                    {
                                        Console.WriteLine(pointName);
                                    }
                                    break;
                                
                                case "-bs":
                                    if (arr.Length != 2)
                                        throw new ShowException();
                                    if (mainBackup == null)
                                        throw new UncreatedBackupException();
                                    Console.WriteLine(mainBackup.BackupSize);
                                    break;
                                
                                case "-tf":
                                    if (arr.Length != 2)
                                        throw new ShowException();
                                    if (mainBackup == null)
                                        throw new UncreatedBackupException();
                                    List<string> paths = mainBackup.GetFiles();
                                    foreach (var path in paths)
                                    {
                                        Console.WriteLine(path);
                                    }
                                    break;
                                
                                case "-pf":
                                    if (mainBackup == null)
                                        throw new UncreatedBackupException();
                                    if (arr.Length != 3)
                                        throw new ShowException();
                                    string name = arr[2];
                                    List<string> pointFiles = mainBackup.GetPointFiles(name);
                                    foreach (var file in pointFiles)
                                    {
                                        Console.WriteLine(file);
                                    }
                                    break;
                                
                                case "-al":
                                    if (mainBackup == null)
                                        throw new UncreatedBackupException();
                                    if (arr.Length != 2)
                                        throw new ShowException();
                                    foreach (var limitName in mainBackup.GetLimitsInfo())
                                    {
                                        Console.WriteLine(limitName);
                                    }
                                    break;
                                
                                default:
                                    throw new ShowException();
                            }
                            break;
                        
                        default:
                            Console.WriteLine("Unknown command");
                            break;
                    }
                }
                catch (MyException ex)
                {
                    Console.WriteLine(ex.Message);
                }
LoopContinue: 
                ;
            }
        }
    }
}