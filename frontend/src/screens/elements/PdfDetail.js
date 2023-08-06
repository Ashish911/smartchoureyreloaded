import React, { useState } from 'react'
import { useTranslation } from 'react-i18next'
import { IoMdCloudDownload } from 'react-icons/io'
import { AiFillEye } from 'react-icons/ai'

const PdfDetail = ({ data, user }) => {

    const {t} = useTranslation()

    const handleDownload = async (url) => {
        try {
            const response = await fetch(url);
            const blob = await response.blob();
            const blobUrl = URL.createObjectURL(blob);
        
            const link = document.createElement('a');
            link.href = blobUrl;
            link.download = 'document.pdf';
            link.click();
    
            URL.revokeObjectURL(blobUrl);
        } catch (error) {
            console.error('Error downloading PDF:', error);
        }
    };

    const handleView = (url) => {
        window.open(url, '_blank');
    };

    return (
        <div className="flex flex-col space-y-4">
            <div className="flex items-start justify-between p-2 border-b border-solid border-slate-200 rounded-t">
                <h3 className="text-2xl font-semibold">
                {t("PDF Detail")}
                </h3>
            </div>
            <div id='photoSection' className='flex'>
                {data?.map((item) => (
                    <div id='onHover' class="w-[200px] sm:w-[250px] md:w-[300px] p-2 mr-2 relative duration-300 transition-all">
                        <object data={item.url} type="application/pdf">
                            <p>PDF preview is not available.</p>
                        </object>
                        {user == false ? 
                            <div id='none' class="absolute top-3 right-5 px-2 py-2 bg-cyan-500 rounded-full cursor-pointer" onClick={() => {
                                handleView(item.url)
                            }}>
                                <AiFillEye className='text-xl text-white'/>
                            </div>
                        : ''}
                        <div id='none' class="absolute top-14 right-5 px-2 py-2 bg-cyan-500 rounded-full cursor-pointer" onClick={() => {
                            handleDownload(item.url)
                        }}>
                            <IoMdCloudDownload className='text-xl text-white'/>
                        </div>
                    </div>
                ))}
            </div> 
        </div>
    )
}

export default PdfDetail