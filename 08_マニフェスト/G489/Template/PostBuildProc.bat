echo このバッチファイルは「08_マニフェスト\G489\G489.csproj」のビルド後に自動的に実行されます。
echo G489.csprojのプロパティ「ビルドイベント」を参照してください。
echo ビルド出力先のパスを引数に取ります。
echo 全産連・6団体版の標準マニテンプレートを出力先にコピーします。
set copySrc=%~dp0manifest_default\*
set copyDst=%1Template
echo コピー元：%copySrc%
echo コピー先：%copyDst%
copy /Y %copySrc% %copyDst%
