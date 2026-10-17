# Modern .NET, Chapter 2, Section 2.5
Get-ChildItem -Recurse -Include *.cs |
  Select-String -Pattern 'BinaryFormatter|WebClient|HttpWebRequest|Thread\.Abort|AppDomain\.Create|System\.Runtime\.Remoting' |
  Group-Object Pattern |
  ForEach-Object { "{0,-30} {1}" -f $_.Name, $_.Count }
