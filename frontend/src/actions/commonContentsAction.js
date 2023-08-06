import axios from 'axios'
import { CREATE_COMMON_CONTENTS_FAIL, CREATE_COMMON_CONTENTS_REQUEST, CREATE_COMMON_CONTENTS_SUCCESS, DELETE_COMMON_CONTENTS_FAIL, DELETE_COMMON_CONTENTS_REQUEST, DELETE_COMMON_CONTENTS_SUCCESS, GET_COMMON_CONTENTS_DETAIL_FAIL, GET_COMMON_CONTENTS_DETAIL_REQUEST, GET_COMMON_CONTENTS_DETAIL_SUCCESS, GET_COMMON_CONTENTS_LIST_FAIL, GET_COMMON_CONTENTS_LIST_REQUEST, GET_COMMON_CONTENTS_LIST_SUCCESS, UPDATE_COMMON_CONTENTS_FAIL, UPDATE_COMMON_CONTENTS_REQUEST, UPDATE_COMMON_CONTENTS_SUCCESS } from '../contsants/commonContentsConstants';
import { DELETE_COMMENT_FAIL, DELETE_COMMENT_REQUEST, DELETE_COMMENT_SUCCESS } from '../contsants/commentConstants';

const apiUrl = process.env.REACT_APP_BASE_URL;

export const getCommonContentsList = () => async (dispatch) => {
    try {
        dispatch({
            type: GET_COMMON_CONTENTS_LIST_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        const { data } = await axios.get(
            apiUrl + 'api/CommonContent/ListPublicUserBoard',
            config
        ) 

        dispatch({
            type: GET_COMMON_CONTENTS_LIST_SUCCESS,
            payload: data
        })
    } catch (error) {
        dispatch({
            type: GET_COMMON_CONTENTS_LIST_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const getCommonContentsDetail = (params) => async (dispatch) => {
    try {
        dispatch({
            type: GET_COMMON_CONTENTS_DETAIL_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
                Authorization: 'Bearer ' + params.token
            }
        };

        const { data } = await axios.get(
            apiUrl + 'api/CommonContent/GetCommonContentDetail?id=' + params.id,
            config
        ) 

        dispatch({
            type: GET_COMMON_CONTENTS_DETAIL_SUCCESS,
            payload: data
        })
    } catch (error) {
        dispatch({
            type: GET_COMMON_CONTENTS_DETAIL_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const createCommonContents = (params) => async (dispatch) => {
    try {
        dispatch({
            type: CREATE_COMMON_CONTENTS_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'multipart/form-data',
                Authorization: 'Bearer ' + params.token
            }
        };

        const formData = new FormData();

        formData.append('Title', params.title)
        formData.append('IsSuccessful', true)
        formData.append('Description', params.description)
        formData.append('IsActive', params.isActive)
        
        params.files.forEach((element) => {
            let blob = new Blob([element], {type: element.type})
            formData.append('files', blob)
        });

        const { data } = await axios.post(
            apiUrl + 'api/CommonContent/InsertOrUpdatePublicUserBoard',
            formData,
            config
        ) 

        dispatch({
            type: CREATE_COMMON_CONTENTS_SUCCESS,
            payload: data
        })
        dispatch(getCommonContentsList())
    } catch (error) {
        dispatch({
            type: CREATE_COMMON_CONTENTS_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const updateCommonContents = (params) => async (dispatch) => {
    try {
        dispatch({
            type: UPDATE_COMMON_CONTENTS_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'multipart/form-data',
                Authorization: 'Bearer ' + params.token
            }
        };

        const formData = new FormData();

        formData.append('PublicUserboardId', params.id)
        formData.append('Title', params.title)
        formData.append('IsSuccessful', true)
        formData.append('Description', params.description)
        formData.append('IsActive', params.isActive)
        
        params.files.forEach((element) => {
            let blob = new Blob([element], {type: element.type})
            formData.append('files', blob)
        });

        const { data } = await axios.post(
            apiUrl + 'api/CommonContent/InsertOrUpdatePublicUserBoard',
            formData,
            config
        ) 

        dispatch({
            type: UPDATE_COMMON_CONTENTS_SUCCESS,
            payload: data
        })
        dispatch(getCommonContentsList())
    } catch (error) {
        dispatch({
            type: UPDATE_COMMON_CONTENTS_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const deleteCommonContents = (params) => async (dispatch) => {
    try {
        dispatch({
            type: DELETE_COMMON_CONTENTS_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
                Authorization: 'Bearer ' + params.token
            }
        };

        const { data } = await axios.delete(
            apiUrl + 'api/CommonContent/DeletPublicUserBoard?publicUserboardId=' + params.id,
            config
        ) 

        dispatch({
            type: DELETE_COMMON_CONTENTS_SUCCESS,
            payload: data
        })
        dispatch(getCommonContentsList())
    } catch (error) {
        dispatch({
            type: DELETE_COMMON_CONTENTS_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}