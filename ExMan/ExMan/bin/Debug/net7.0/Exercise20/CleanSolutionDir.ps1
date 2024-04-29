Get-ChildItem -Path $PSScriptroot -Recurse -Force  -Include '.vs', '.git', 'bin', 'obj', '.idea', '*.Dotsettings', '*.Dotsettings.user' | ForEach-Object {
    Remove-Item -Recurse -Force -LiteralPath $_.FullName >$null
}
