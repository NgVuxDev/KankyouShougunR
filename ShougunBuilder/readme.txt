【ShougunBuilder解説】
初版		：2014/03/25
最終更新	：2014/04/08


【ファイル構成】
- ShougunBuilder
	├ log
	│	⇒ShougunBuilder実行時のログが格納されます。
	│
	├ BaseSolution.sln
	│	⇒開発用ソリューションです。
	│
	├ CopyFileInfo.xml
	│	⇒ビルドしてもコピーされない、実行に必要なファイルを記述しています。
	│
	├ DllInfo.xml
	│	⇒ローカルコピーされて欲しいDLLのパスと名前を記述しています。
	│
	├ MakeRSolution.exe
	│	⇒BaseSolution.slnを構築する実行ファイルです。
	│	※ShougunBuilder.batが利用します。直接実行しないでください。
	│
	├ MakeVersionInfoTemplate.exe
	│	⇒バージョン情報の更新を行うコマンドです。
	│	※UpdateVersion.batが利用します。直接実行しないでください。
	│
	├ ProjectInfo.xml
	│	⇒ビルド対象となるプロジェクトのパスを記述しています。
	│
	├ ShougunBuilder.bat
	│	⇒ビルド実行バッチファイルです。ビルドを行うには、こちらを実行してください。
	│
	├ UpdateVersion.bat
	│	⇒バージョン情報を更新するバッチファイルです。ビルドの最後に実行します。
	│
	├ vsvars32.bat
	│	⇒コマンドラインビルドを行うために必要な環境設定を行うバッチです。
	│	※ShougunBuilder.batが利用します。直接実行しないでください。
	│
	└ readme.txt
		⇒当ファイルです。


【ビルド実行方法】
1. 下記をチェックアウト
　①http://developers.e-mall.co.jp/subversion/r-shougun/trunk

～ShougunBuilder.batを用いてDebug/Release両方同時に作成する場合～
2. r-shougun/trunk/ShougunBuilderの"ShougunBuilder.bat"をエディタで開き、下記のディレクトリの設定を行う
	⇒Debug環境出力ディレクトリ名(DBG_OUT_DIR)
	⇒Release環境出力ディレクトリ名(REL_OUT_DIR)
	⇒ProjectInfo.xml配置ディレクトリ名(PRJ_INFO_DIR)
	※デフォルトはそれぞれ下記に設定されています
		DBG_OUT_DIR		：Debug
		REL_OUT_DIR		：Release
		PRJ_INFO_DIR	：ShougunBuilder.batと同階層

3. "ShougunBuilder.bat"をコマンドプロンプトから実行
	⇒同階層にBaseSolution.slnファイルとDebug/Release実行ファイルが作成されます。
	⇒ソリューションファイル作成もしくはビルドはプロンプト上でスキップする事も可能です。
	※↓↓ShougunBuilder.batオプション↓↓
	>> ShougunBuilder.bat [Option1] [Option2]...
		[Option]	│
			-g		│ 各プロジェクトのGUIDを再設定します。
			-f		│ ビルド突入時に確認を行いません。
			-h		│ Helpを表示します。

～BaseSolution.slnよりビルド対象を任意のものにしたい場合～
2. BaseSolution.slnを開く
	⇒スタートアッププロジェクトをログイン画面(G451)に設定
	⇒ソリューション構成をDebug(もしくはRelease)に変更
	⇒ビルド
	⇒同階層に選択したソリューション構成の実行ファイルが作成されます。

