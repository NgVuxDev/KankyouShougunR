echo --------------------------------------------------------------
echo [ PostBuildProc.bat ]
echo このバッチファイルはShougun.Printing.VerUpプロジェクトのビルドポストプロセスで実行されます。
echo バッチ内からTortoiseSVNのSubWCRevコマンドを呼び出します。
echo Shougun.Printing.Revision.txtにShougun.Printingディレクトリのリビジョン番号を埋め込みビルド出力パスにコピーします。
echo Shougun.Printing.Revision.txtはクラウド/クライアント側印刷PGのバージョンアップの判定に利用されます。

set WorkingCopyPath=%~dp0..
set SrcVersionFile=%~dp0Shougun.Printing.Revision.txt
set DstVersionFile=%1Shougun.Printing.Revision.txt

echo;
echo WorkingCopyPath = %WorkingCopyPath%
echo SrcVersionFile  = %SrcVersionFile%
echo DstVersionFile  = %DstVersionFile%
echo;

SubWCRev %WorkingCopyPath% %SrcVersionFile% %DstVersionFile%

echo;
echo 結果(%DstVersionFile%)
type %DstVersionFile%
echo;
echo --------------------------------------------------------------
