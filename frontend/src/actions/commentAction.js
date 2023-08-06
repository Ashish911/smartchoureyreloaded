import { ADD_COMMENT_FAIL, ADD_COMMENT_REQUEST, ADD_COMMENT_SUCCESS, DELETE_COMMENT_FAIL, DELETE_COMMENT_REQUEST, DELETE_COMMENT_SUCCESS, GET_COMMENT_BY_MEDIA_FAIL, GET_COMMENT_BY_MEDIA_REQUEST, GET_COMMENT_BY_MEDIA_SUCCESS } from "../contsants/commentConstants";

import axios from 'axios'

const apiUrl = process.env.REACT_APP_BASE_URL;

export const getCommentsListByMediaId = (params) => async (dispatch) => {
    try {
        dispatch({
            type: GET_COMMENT_BY_MEDIA_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        const { data } = await axios.get(
            apiUrl + 'api/MediaComment/getCommentsByMediaId?choureyId=' + params.choureyId 
            + '&category=' + params.category + '&requestedBy=asd&mediaId=' + params.mediaId,
            config
        ) 

        dispatch({
            type: GET_COMMENT_BY_MEDIA_SUCCESS,
            payload: data
        })
    } catch (error) {
        dispatch({
            type: GET_COMMENT_BY_MEDIA_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const deleteCommentById = (params) => async (dispatch) => {
    try {
        dispatch({
            type: DELETE_COMMENT_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        const { data } = await axios.delete(
            apiUrl + 'api/MediaComment/deleteComment?commentId=' + params.commentId,
            config
        ) 

        dispatch({
            type: DELETE_COMMENT_SUCCESS,
            payload: data
        })

        let getParams = {
            choureyId: params.id,
            category: params.type,
            mediaId: params.fileId
        }
        dispatch(getCommentsListByMediaId(getParams))
    } catch (error) {
        dispatch({
            type: DELETE_COMMENT_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const addCommentUser = (params) => async (dispatch) => {
    try {
        dispatch({
            type: ADD_COMMENT_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
                Authorization: 'Bearer ' + params.token
            }
        };

        const { data } = await axios.post(
            apiUrl + 'api/MediaComment/addComment',
            {
                comment: params.comment,
                eCategory: params.type,
                choureyId: params.id,
                choureyMediaId: params.fileId,
                eUploadType: params.fileType,
                createdBy: params.userId,
                eDeviceType: 3
            },
            config
        ) 

        dispatch({
            type: ADD_COMMENT_SUCCESS,
            payload: data
        })

        let getParams = {
            choureyId: params.id,
            category: params.type,
            mediaId: params.fileId
        }
        dispatch(getCommentsListByMediaId(getParams))
    } catch (error) {
        dispatch({
            type: ADD_COMMENT_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}