import React, { useState } from 'react'
import { useTranslation } from 'react-i18next'
import { IoMdCloudDownload } from 'react-icons/io'
import { AiOutlineComment } from 'react-icons/ai'
import Comment from './Comment'
import { useEffect } from 'react'

const PhotoDetail = ({data , user, type, id}) => {

    const {t} = useTranslation()
    const [showModal, setShowModal] = useState(false)
    const [selected, setSelected] = useState('')

    const handleDownload = (url) => {
        const link = document.createElement('a');
        link.href = url;
        link.download = 'photo.jpg';
        link.click();
    };

    const addComment = (uniqueId) => {
        if (showModal == true) {
            setSelected('')
            setShowModal(false)
        } else {
            setSelected(uniqueId)
            setShowModal(true)
        }
    }

    return (
        <>
            <div className="flex flex-col space-y-4">
                <div className="flex items-start justify-between p-2 border-b border-solid border-slate-200 rounded-t">
                    <h3 className="text-2xl font-semibold">
                    {t("Photo Detail")}
                    </h3>
                </div>
                <div id='photoSection' className='flex'>
                    {data?.map((item) => (
                        <div id='onHover' class="w-[200px] sm:w-[250px] md:w-[300px] p-2 mr-2 relative duration-300 transition-all">
                            <img src={item.url} alt="" />
                                <div id='none' class="absolute top-3 right-5 px-2 py-2 bg-cyan-500 rounded-full cursor-pointer" onClick={() => {
                                    addComment(item.uniqueId)
                                }}>
                                    <AiOutlineComment className='text-xl text-white'/>
                                </div>
                                <div id='none' class="absolute top-14 right-5 px-2 py-2 bg-cyan-500 rounded-full cursor-pointer" onClick={() => {
                                    handleDownload(item.url)
                                }}>
                                    <IoMdCloudDownload className='text-xl text-white'/>
                                </div>
                        </div>
                    ))}
                </div> 
            </div>
            {showModal ? (
                <>
                    <Comment addComment={addComment} selectedId={selected} type={type} id={id} fileType={1}/>
                </>
            ) : null }
        </>
    )
}

export default PhotoDetail