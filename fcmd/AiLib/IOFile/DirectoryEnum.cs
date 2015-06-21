using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AiLib.IOFile
{
    public static class DirectoryEnum
    {
        public static IEnumerable<string> ReadFiles(string path,
                String searchPattern = "*.*", SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            var resultHandler = new StringResultHandler(true, includeDirs: false);
            var iterator = new Win32FileSystemEnumerableIterator<string>(path, null, searchPattern, searchOption, resultHandler);
            var numer = iterator.GetEnumerator();

            while (numer.MoveNext())
                yield return Path.Combine(path, numer.Current);
        }

        public static IEnumerable<FileDataInfo> ReadFilesInfo(string path,
                       String searchPattern = "*.*", SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            var resultHandler = new FileDataInfoResultHandler();
            var iterator = new Win32FileSystemEnumerableIterator<FileDataInfo>(path, null, searchPattern, searchOption, resultHandler);
            var numer = iterator.GetEnumerator();

            while (numer.MoveNext())
                yield return numer.Current;
        }

        public class FileDataInfo
        {
            internal uint dwFileAttributes;
            internal Win32FindFile.FILE_TIME ftLastWriteTime;
            internal uint nFileSizeLow;

            //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            internal string cFileName;

            public string Name { get { return cFileName; } }
            public int Length { get { return (int)nFileSizeLow; } }
            public DateTime LastWriteTime
            {
                get { return DateTime.FromFileTime(ftLastWriteTime.ToTicks()); }
            }
        }

        internal class FileDataInfoResultHandler : SearchResultHandler<FileDataInfo>
        {
            [System.Security.SecurityCritical]
            internal override bool IsResultIncluded(SearchResult result)
            {
                return Win32FileSystemEnumerableHelpers.IsFile(result.FindData);
            }

            [System.Security.SecurityCritical]
            internal override FileDataInfo CreateObject(SearchResult result)
            {
                Win32FindFile.WIN32_FIND_DATA data = result.FindData;
                var build = new StringBuilder(data.cFileName);
                FileDataInfo fi = new FileDataInfo
                {
                    cFileName = build.ToString(),
                    dwFileAttributes = data.dwFileAttributes,
                    nFileSizeLow = data.nFileSizeLow,
                    ftLastWriteTime = data.ftLastWriteTime
                };
                return fi;
            }
        }
    }

}