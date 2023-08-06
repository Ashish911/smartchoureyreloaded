import React, { useEffect } from 'react'
import { useState } from 'react'
import { AiOutlineDelete } from 'react-icons/ai'

const VideoEditDisplay = ({data, type, categoryId, setDeleteModal, setData}) => {

    const [videoData, setVideoData] = useState([])
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
        setVideoData(datas)
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
                type: 2,
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

    return (
        <div id='photoSection' className='flex'>
            {videoData?.map((item) => (
                <div id='onHover' class="w-[200px] sm:w-[250px] md:w-[300px] p-2 mr-2 relative duration-300 transition-all">
                    <video controls>
                        <source src={item.url} type="video/mp4" />
                        Your browser does not support the video tag.
                    </video>
                    {/* <img src={item.url} alt="" /> */}
                        <div id='none' class="absolute top-3 right-5 px-2 py-2 bg-red-500 rounded-full cursor-pointer" onClick={() => {
                            deleteFunction(item.id, item.uniqueId)
                        }}>
                            <AiOutlineDelete className='text-xl text-white'/>
                        </div>
                </div>
            ))}
        </div>
    )
}

export default VideoEditDisplay