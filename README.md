# MWImageViewer

シンプルなWPF製の画像ビューアーです。

## 概要

MWImageViewerは、画像ファイルの表示、ドラッグ＆ドロップ、クリップボードへのコピー、クリップボードからの貼り付けに対応した画像ビューアーです。

## 主な機能

- 画像ファイルを開く
- 画像ファイルのドラッグ＆ドロップ
- クリップボードへ画像をコピー
- クリップボードから画像を貼り付け
- 画像を96DPIへ変換して表示
- PNG形式での画像保存処理

## 対応画像形式

- PNG
- JPEG / JPG
- BMP
- GIF
- TIFF / TIF
- WebP

## ショートカット

| 操作 | ショートカット |
|---|---|
| 画像を開く | Ctrl + O |
| 画像をコピー | Ctrl + C |
| 画像を貼り付け | Ctrl + V |

## 必要環境

- Windows
- .NET 10.0
- WPF

## ビルド方法

```bash
git clone https://github.com/kareteruhito/MWImageViewer.git
cd MWImageViewer
dotnet build
