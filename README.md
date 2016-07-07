[![Stories in Ready](https://badge.waffle.io/HassakuTb/KiritanAction.png?label=ready&title=Ready)](https://waffle.io/HassakuTb/KiritanAction)
きりたんのアクションゲーム(仮)
====

[![Join the chat at https://gitter.im/HassakuTb/KiritanAction](https://badges.gitter.im/HassakuTb/KiritanAction.svg)](https://gitter.im/HassakuTb/KiritanAction?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)
<img src="https://github.com/HassakuTb/KiritanAction/blob/gh-resources/zzm_kiritan_fly2.png" alt="きりたん" title="きりたん" width="320px" align="right">  

note: This is fan game of [Tohoku Zunko](http://zunko.jp/) powered by Unity. Work in progress. This document is written in Japanese. If you needed English information, please contact to [Hassaku](https://github.com/hassakuTb) directly.

## Overview
[東北ずん子](http://zunko.jp/)の二次創作ゲームです。Unityで動きます。
製作中であり完成品ではありません。
東北きりたんを操作するステージクリア型のサイドビュー2Dアクションゲームを想定しています。
<BR clear="right">
***


## Play
[http://HassakuTb.github.io/KiritanAction/](http://HassakuTb.github.io/KiritanAction/)  
不定期にWebGLでビルドします。

### 操作方法
* ←→ 歩く
* ↓ 構える
* z ジャンプ
* x 砲撃

## Requirement
* Unity3D Version 5.3.5f1 Personal (他環境での動作は確認していません)

## Usage
Unityを起動し、File → Open Projectでルートディレクトリを指定します。
シーンファイルは/Assets/Scenes以下にあります。

音源データは一部を除きリポジトリに含まれません。

## Contribution
* 大歓迎 :revolving_hearts: 一緒に作りましょう。
* [東北ずん子のガイドライン](http://zunko.jp/guideline.html)を一読し、違反の無いようにしましょう。

### 提案、質問、バグ報告したい
* あなたはとてもいい人だ！
* 純粋な質問や「こうしたほうが良いんじゃないの？」みたいな提案は気軽にIssueにPOSTしてね。
* Issueの使い方がわからなくてもとりあえずIssuesのタブをクリックしてNew Issueのボタンを押してみると良いよ。
* Issueが貰えるととても喜びます。
* 直接連絡してくれてもいいですよ。

### 変更を加えたい、素材を提供したい(Git分かる人向け)
* あなたは神だ！
* Pull Requestを作成してください。
  1. このリポジトリをフォークします。
  2. ローカルリポジトリにクローンします。(git clone [URL])
  3. 作業ヘッドをdevelopブランチに移動します。(git checkout develop)
  4. 変更用のトピックブランチを切ります。(git checkout -b feature/hogehoge)
  5. 変更を加えてコミットします。(git commit message)
  6. トピックブランチをフォークしたリポジトリにプッシュします。(git push origin feature/hogehoge)
    * pushの前にrebaseでコミットを整理するとより良いですよ。
  7. Pull Requestを作成します。マージ先はdevelopにしてください。
  8. マージがOKになったらマージします。もし衝突が発生したらがんばって解消します。
    * シーンファイル(*.unity)ファイルが衝突すると解消は結構大変です:boom::confused:
    * 衝突した場合は[UnityYAMLMerge](http://docs.unity3d.com/jp/current/Manual/SmartMerge.html)で多少なんとかなります。
  9. おめでとう！:+1:
* push権限が欲しい(リポジトリを直接さわりたい)場合は連絡ください。collaboratorとして登録します。

### 変更を加えたい、素材を提供したい(Git分からない人向け)
* あなたも神だ！
* 直接連絡くださいお願いします。

### 連絡先
* Hassaku
  * [GitHub(mail)](https://github.com/hassakuTb)
  * [Twitter](https://twitter.com/HassakuTb)

## License
* ソースコード(*.cs)は[MITライセンス](https://github.com/HassakuTb/KiritanAction/blob/master/Licenses/MIT.txt)です。
* Noto Sans CJK JP Regularフォントファイル(/Assets/Fonts/NotoSansCJKjp-Regular_16.otf)は[SIL OPEN FONT LICENSE Version 1.1](https://github.com/HassakuTb/KiritanAction/blob/master/Licenses/SIL_Open_Font_License_1.1.txt)です。
* JFドットM+H10フォントファイル(/Assets/Fonts/JF-Dot-MPlusH10B.ttf)は[M+フォントライセンス](https://github.com/HassakuTb/KiritanAction/blob/master/Licenses/MPlus_Font_License.txt)です。
* /Assets/Sprites/Zunkyo以下の画像ファイルは[東北ずん子ガイドライン](http://zunko.jp/guideline.html)準拠です。
* 以上を除くアセットファイルは[CC-by 4.0](https://creativecommons.org/licenses/by/4.0/legalcode)です。
* gh-pagesブランチにビルドされた成果物は再頒布が禁止です。(複製物または二次的著作物を含む)
