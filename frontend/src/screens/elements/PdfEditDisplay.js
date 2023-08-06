import React, { useEffect } from 'react'
import { useState } from 'react'
import { AiOutlineDelete } from 'react-icons/ai'
import { IoMdCloudDownload } from 'react-icons/io'

const PdfEditDisplay = ({data, type, categoryId, setDeleteModal, setData}) => {

    const [pdfData, setPdfData] = useState([])
    var siteId = localStorage.getItem('siteId');
    var userInfo = localStorage.getItem('userInfo');

    let datas = []

    useEffect(() => {
        if (data) {
            datas = []
            let no = 1
            data.map((element) => {
                let obj = {
                    id: no,
                    url: element.url,
                    uniqueId: element.uniqueId
                }
                no++
                datas.push(obj)
            })
        }
        setPdfData(datas)
    }, [data])

    const deleteFunction = (id, fileId) => {
        if (id != undefined) {
            setDeleteModal(true)
            let params = {
                fileId : fileId,
                siteId : siteId,
                categoryId : categoryId,
                category : 0,
                fileType : 1,
                type: 3,
                token: JSON.parse(userInfo).token
            }

            if (type == 'ChoureyOne') {
                params.category = 1
            } else if (type == 'ChoureyTwo') {
                params.category = 2
            } else {
                params.category = 3
            }
            setData(params)
        }
    }

    const downloadFunction = async (url) => {
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
    }

    return (
        <div id='photoSection' className='flex'>
            {pdfData?.map((item) => (
                <div id='onHover' class="w-[200px] sm:w-[250px] md:w-[300px] p-2 mr-2 relative duration-300 transition-all">
                    <object data={item.url} type="application/pdf">
                        <p>PDF preview is not available.</p>
                    </object>
                        <div id='none' class="absolute top-3 right-5 px-2 py-2 bg-red-500 rounded-full cursor-pointer" onClick={() => {
                            deleteFunction(item.id, item.uniqueId)
                        }}>
                            <AiOutlineDelete className='text-xl text-white'/>
                        </div>
                        <div id='none' class="absolute top-14 right-5 px-2 py-2 bg-cyan-500 rounded-full cursor-pointer" onClick={() => {
                            downloadFunction(item.url)
                        }}>
                            <IoMdCloudDownload className='text-xl text-white'/>
                        </div>
                </div>
            ))}
        </div>
    )
}

export default PdfEditDisplay