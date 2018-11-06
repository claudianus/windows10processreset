//claudianus@engineer.com

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal static class Program
    {
        private static readonly string[] whitelist = {
            //종료하지 않을 프로세스 화이트리스트 (윈도우10 기준)
            "System",
            "Idle",
            "svchost",
            "smss",
            "RuntimeBroker",
            "csrss",
            "wininit",
            "services",
            "winlogon",
            "lsass",
            "dwm",
            "fontdrvhost",
            "spoolsv",
            "MsMpEng",
            "dllhost",
            "WmiPrvSE",
            "sihost",
            "taskhostw",
            "msdtc",
            "SearchIndexer",
            "TrustedInstaller",
            "explorer",
            "CompatTelRunner",
            "SearchUI",
            "NisSrv",
            "ShellExperienceHost",
            "TiWorker",
            "MSASCuiL",
            "installAgentUserBroker",
            "conhost",
            "InstallAgent",
            "Memory Compression",
            "SecurityHealthService",
            "audiodg",
            "BackgroundTransferHost"
        };

        private static void Main(string[] args)
        {
            //콘솔창 이름 설정
            Console.Title = "윈도우 10 프로세스 초기화 도구";

            Console.WriteLine("프로세스를 초기화 하시겠습니까? y/n");

            //문자열 입력 받기
            var input = Console.ReadLine();
            if (input.ToLowerInvariant() != "y")
            {
                // 사용자가 입력한 값이 y이 아니면 종료
                return;
            }

            //사용자가 입력한 값이 y이면...

            //실행중인 프로세스들 가져오기
            var processes = Process.GetProcesses();

            //현재 프로세스 가져오기
            var currentProcess = Process.GetCurrentProcess();

            //각각의 프로세스에 대한 작업 반복
            foreach (var process in processes)
            {
                //현재 프로세스라면
                if (process.Id == currentProcess.Id)
                {
                    //이 프로세스에 대한 작업 스킵
                    continue;
                }

                //화이트리스트에 존재하지않는 프로세스라면
                if (!whitelist.Contains(process.ProcessName))
                {
                    //예외(오류)처리 문
                    try
                    {
                        //해당 프로세스 종료
                        process.Kill();

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"{process.ProcessName} 종료됨.");
                        Console.ResetColor();
                    }
                    catch (Exception error /*발생한 오류에 대한 정보가 error 변수에 담김*/)
                    {
                        //try문의 블럭 내부에서 오류 발생시 이 부분의 코드가 실행 됨

                        //오류 메세지 출력
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine($"{process.ProcessName} 종료실패: {error.Message}");
                        Console.ResetColor();
                    }
                }
            }

            //작업 끝
            Console.WriteLine("작업 끝");
            Console.ReadLine();
        }
    }
}
