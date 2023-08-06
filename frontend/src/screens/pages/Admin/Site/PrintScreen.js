import React, { useEffect, useRef, useState } from 'react'
import { useTranslation } from 'react-i18next'
import * as bsIcon from "react-icons/bs";
import * as aiIcon from "react-icons/ai";
import QRCode from 'react-qr-code'
import access from '../../../../Images/access.jpg';
import shortcutIphone from '../../../../Images/shortcutIphone.jpg';
import shortcutAndroid from '../../../../Images/shortcutAndroid.jpg';
import stepOne from '../../../../Images/stepOne.jpg';
import stepTwo from '../../../../Images/stepTwo.jpg';
import stepThree from '../../../../Images/stepThree.jpg';

const PrintScreen = ({closeFunction, qrValue}) => {

    const {t} = useTranslation()

    const [currentDiv, setCurrentDiv] = useState('qr')

    const printFunction = () => {
        let divId = "printScreen"
        var printContents = document.getElementById(divId).cloneNode(true);
        var printContainer = document.createElement('div');
        printContainer.appendChild(printContents);
    
        var iframe = document.createElement('iframe');
        iframe.style.display = 'none';
        document.body.appendChild(iframe);
    
        var iframeDocument = iframe.contentWindow.document;
        iframeDocument.body.appendChild(printContainer);
    
        var link = iframeDocument.createElement('link');
        link.href = 'path/to/your/external-styles.css';
        link.rel = 'stylesheet';
        iframeDocument.head.appendChild(link);
    
        link.onload = function () {
            iframe.contentWindow.print();
            document.body.removeChild(iframe);
        };
    }

    return (
        <>
            <div 
                id='printScreen'
                className=" justify-center items-center flex overflow-x-hidden overflow-y-auto fixed inset-0 z-50 outline-none focus:outline-none"
            >
                <div className="relative w-5/6 my-6 mx-auto max-w-5xl">
                    <div className="border-0 rounded-lg shadow-lg relative flex flex-col w-full bg-white outline-none focus:outline-none">
                        <div className="flex items-start justify-between p-5 border-b border-solid border-slate-200 rounded-t">
                        <h3 className="text-3xl font-semibold">
                        このページを印刷して現場に設置してください。
                        </h3>
                        <button
                            className="p-1 ml-auto bg-transparent border-0 text-black float-right text-3xl leading-none font-semibold outline-none focus:outline-none"
                            onClick={closeFunction}
                        >
                            <span className="bg-transparent text-black h-6 w-6 text-2xl block outline-none focus:outline-none">
                            ×
                            </span>
                        </button>
                        </div>
                        <div className="relative p-6 flex flex-col ">
                            <p>スマート朝礼　閲覧方法</p>
                            <p>現場：〇✕△□新築工事 (ベルーナテスト） ....現場　閲覧時間（00:00～23:59）</p>
                        </div>
                        <div className='p-4'>
                            <ul className='flex'>
                                <li className={currentDiv == 'qr' ? "current bg-cyan-500 text-white p-2 rounded-l border" : "bg-white text-black p-2 rounded-l border"} id='site'><a className='cursor-pointer' onClick={() => setCurrentDiv('qr')}>{t('QR')}</a></li>
                                <li className={currentDiv == 'stepGuide' ? "current bg-cyan-500 text-white p-2 rounded-r border" : "bg-white text-black p-2 rounded-r border"} id='user'><a className='cursor-pointer' onClick={() => setCurrentDiv('stepGuide')}>{t('Step Guide')}</a></li>
                            </ul>
                        </div>
                        { currentDiv == 'qr' ?
                        (
                            <QrScreen qrValue={qrValue}/>
                        ) : 
                        (
                            <GuideScreen />
                        ) 
                        }
                        <div className="flex items-center justify-start p-6 border-t border-solid border-slate-200 rounded-b">
                            <button
                                className="flex flex-row items-center text-white bg-cyan-500 font-bold uppercase px-6 py-2 text-sm outline-none focus:outline-none mr-1 mb-1 ease-linear transition-all duration-150 rounded-md shadow"
                                type="button"
                                onClick={printFunction}
                            >
                                する
                                <bsIcon.BsPrinter className='ml-2'/>
                            </button>
                            <button
                                className="flex flex-row items-center text-white bg-cyan-500 font-bold uppercase px-6 py-2 text-sm outline-none focus:outline-none mr-1 mb-1 ease-linear transition-all duration-150 rounded-md shadow"
                                type="button"
                                onClick={closeFunction}
                            >
                                タブを閉じる
                                <aiIcon.AiOutlineCloseCircle className='ml-2' />
                            </button>
                        </div>
                    </div>
                    </div>
                </div>
            <div className="opacity-25 fixed inset-0 z-40 bg-black"></div>
        </>
    )
}

function QrScreen({qrValue}) {
    return (
        <div className="relative p-6 flex flex-row">
            <div className='flex flex-col sm:flex-row p-2'>
                <div className='flex flex-col items-center mb-4'>
                    <h3 className="text-xl mb-2 font-semibold">本日の現場QRコード</h3>
                    <QRCode
                        size={225}
                        bgColor="white"
                        fgColor='black'
                        value={qrValue}
                    />
                </div>
                <div className='flex flex-1 flex-col items-center mb-4'>
                    <h3 className="text-xl mb-2 font-semibold">アプリをホーム画面に追加</h3>
                    <div className='flex flex-1 flex-row items-center mb-4 ml-4'>
                        <div>
                            <img src={shortcutIphone} alt="React Image" />
                        </div>
                        <div>
                            <img src={shortcutAndroid} alt="React Image" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}

function GuideScreen() {
    return (
        <div className="relative px-6 pb-6 flex flex-row">
            <div className='flex flex-col items-center p-2'>
                <h3 className="text-xl mb-2 font-semibold">アクセス方法</h3>
                <div className='flex flex-col sm:flex-row p-2'>
                    <div>
                        <img src={stepOne} alt="React Image" />
                    </div>
                    <div>
                        <img src={stepTwo} alt="React Image" />
                    </div>
                    <div>
                        <img src={stepThree} alt="React Image" />
                    </div>
                </div>
                <aiIcon.AiFillDownCircle className="text-xl" />
                <h3 className="text-xl mb-2 mt-4">朝礼内容を確認した後は</h3>
                <h3 className="text-xl mb-2 ">必ず下部右端のアイコンからログアウトしてください。</h3>
            </div>
        </div>
    )
}

export default PrintScreen