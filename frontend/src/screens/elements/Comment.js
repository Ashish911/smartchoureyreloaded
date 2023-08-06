import React, { useEffect } from 'react'
import { useState } from 'react'
import { useTranslation } from 'react-i18next'
import { useDispatch, useSelector } from 'react-redux';
import { addCommentUser, deleteCommentById, getCommentsListByMediaId } from '../../actions/commentAction'
import { AiOutlineDelete } from 'react-icons/ai'
import { GET_COMMENT_BY_MEDIA_RESET } from '../../contsants/commentConstants';

const Comment = ({ addComment, selectedId, type, id, fileType }) => {

    const {t} = useTranslation()
    const [text, setText] = useState('')
    const dispatch = useDispatch();
    const commentInfo = useSelector((state) => state.comment);
    const [data, setData] = useState([])
    const { getComments, deleteComments, addComments } = commentInfo
    var userInfo = localStorage.getItem('userInfo');
    let commentData = []

    const closeModal = () => {
        dispatch({type: GET_COMMENT_BY_MEDIA_RESET})
        addComment()
    }

    const postComment = () => {
        let params = {
            comment: text,
            type: type == "choureyOne" ? 1 : type == "choureyTwo" ? 2 : 3,
            id: id,
            fileId: selectedId,
            fileType: fileType,
            userId: JSON.parse(userInfo).id,
            token: JSON.parse(userInfo).token
        }
        
        dispatch(addCommentUser(params))
        setText('')
    }

    const deleteComment = (item) => {
        let params = {
            commentId: item.id,
            id: id,
            type: type == "choureyOne" ? 1 : type == "choureyTwo" ? 2 : 3,
            fileId: selectedId
        }
        
        dispatch(deleteCommentById(params))
    }

    useEffect(() => {
        let params = {
            choureyId: id,
            category: type == "choureyOne" ? 1 : type == "choureyTwo" ? 2 : 3,
            mediaId: selectedId
        }
        dispatch(getCommentsListByMediaId(params))
    }, [])

    useEffect(() => {
        if (getComments) {
            if (getComments.list) {
                commentData = []
                    let no = 1
                    getComments.list.map((comment) => {
                        let obj = {
                            sNo: no,
                            id: comment.choureyMediaCommentId,
                            comment: comment.comment,
                            user: comment.createdBy,
                            date: comment.createdOn,
                            isUser: JSON.parse(userInfo).id == comment.createdBy ? true : false
                        }
                        no++
                        commentData.push(obj)
                    })
                setData(commentData)
            } 
        }
    }, [getComments])

    return (
        <>
            <div
                className="justify-center items-center flex overflow-x-hidden overflow-y-auto fixed inset-0 z-50 outline-none focus:outline-none"
            >
                <div className="relative w-4/6 my-6 mx-auto max-w-xl">
                    <div className="border-0 rounded-lg shadow-lg relative flex flex-col w-full bg-white outline-none focus:outline-none">
                        <div className="flex items-start justify-between p-5 border-b border-solid border-slate-200 rounded-t">
                        <h3 className="text-3xl font-semibold">
                        {t("Comment")}
                        </h3>
                        <button
                            className="p-1 ml-auto bg-transparent border-0 text-black float-right text-3xl leading-none font-semibold outline-none focus:outline-none"
                            onClick={closeModal}
                        >
                            <span className="bg-transparent text-black h-6 w-6 text-2xl block outline-none focus:outline-none">
                            ×
                            </span>
                        </button>
                        </div>

                        <div className='overflow-y-auto max-h-96'>
                            {data.map((item) => (
                                <div class="flex px-6 py-3">
                                    <div class="flex flex-1 items-center w-full">
                                        <div class="rounded-xl px-4 py-2 w-full bg-gray-100 text-black">
                                            <div class="font-medium flex items-center justify-between">
                                                <p className='text-sm'><span className='font-bold text-lg'> {item.user == null ? 'User' : item.user}</span> [{item.date}]</p>
                                                {item.user == JSON.parse(userInfo).email ? 
                                                    <AiOutlineDelete className='text-lg hover:text-cyan-500 cursor-pointer' onClick={() => {
                                                        deleteComment(item)
                                                    }}/>
                                                :
                                                    ''
                                                }
                                            </div>
                                            <div class="text-sm flex justify-star">
                                                {item.comment}
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            ))}
                        </div>
                        
                        <div className="relative p-6 flex ">
                            <div class="flex md:flex-1 flex-col space-y-1">
                                <input
                                    placeholder='Please write a comment'
                                    type="text"
                                    id="text"
                                    value={text}
                                    onChange={(e) => setText(e.target.value)}
                                    autoFocus
                                    class="px-4 py-2 transition duration-300 border border-gray-300 rounded focus:border-transparent focus:outline-none focus:ring-4 focus:ring-cyan-200"
                                />
                            </div>
                        </div>
                        <div className="flex items-center justify-start p-6 border-t border-solid border-slate-200 rounded-b">
                            <button
                                className="text-white bg-cyan-500 font-bold uppercase px-6 py-2 text-sm outline-none focus:outline-none mr-1 mb-1 ease-linear transition-all duration-150 rounded-md shadow"
                                type="button"
                                onClick={postComment}
                            >
                                <p>{t("Add Comment")}</p>
                            </button>
                        </div>
                    </div>
                    </div>
                </div>
            <div className="opacity-25 fixed inset-0 z-40 bg-black"></div>
        </>
    )
}

export default Comment