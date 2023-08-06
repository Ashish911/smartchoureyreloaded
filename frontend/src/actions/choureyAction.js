import { CHOUREY_ONE_DELETE_FAIL, CHOUREY_ONE_DELETE_REQUEST, CHOUREY_ONE_DELETE_SUCCESS, CHOUREY_ONE_DETAIL_FAIL, CHOUREY_ONE_DETAIL_REQUEST, CHOUREY_ONE_DETAIL_SUCCESS, CHOUREY_TWO_DELETE_FAIL, CHOUREY_TWO_DELETE_REQUEST, CHOUREY_TWO_DELETE_SUCCESS, CHOUREY_TWO_DETAIL_FAIL, CHOUREY_TWO_DETAIL_REQUEST, CHOUREY_TWO_DETAIL_SUCCESS, CREATE_CHOUREY_ONE_FAIL, CREATE_CHOUREY_ONE_REQUEST, CREATE_CHOUREY_ONE_SUCCESS, CREATE_CHOUREY_TWO_FAIL, CREATE_CHOUREY_TWO_REQUEST, CREATE_CHOUREY_TWO_RESET, CREATE_CHOUREY_TWO_SUCCESS, GET_CHOUREY_ONE_LIST_FAIL, GET_CHOUREY_ONE_LIST_REQUEST, GET_CHOUREY_ONE_LIST_SUCCESS, GET_CHOUREY_TWO_LIST_FAIL, GET_CHOUREY_TWO_LIST_REQUEST, GET_CHOUREY_TWO_LIST_SUCCESS, MULTIPLE_CHOUREY_ONE_DELETE_FAIL, MULTIPLE_CHOUREY_ONE_DELETE_REQUEST, MULTIPLE_CHOUREY_ONE_DELETE_SUCCESS, MULTIPLE_CHOUREY_TWO_DELETE_FAIL, MULTIPLE_CHOUREY_TWO_DELETE_REQUEST, MULTIPLE_CHOUREY_TWO_DELETE_SUCCESS, UPDATE_CHOUREY_ONE_FAIL, UPDATE_CHOUREY_ONE_REQUEST, UPDATE_CHOUREY_ONE_SUCCESS, UPDATE_CHOUREY_TWO_FAIL, UPDATE_CHOUREY_TWO_REQUEST, UPDATE_CHOUREY_TWO_SUCCESS } from '../contsants/choureyConstants'
import axios from 'axios'

const apiUrl = process.env.REACT_APP_BASE_URL;

export const getChoureyOneList = (siteId) => async (dispatch) => {
    try {
        dispatch({
            type: GET_CHOUREY_ONE_LIST_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        const { data } = await axios.get(
            apiUrl + 'api/Setup/listChoureyOne?siteId=' + siteId,
            config
        ) 

        dispatch({
            type: GET_CHOUREY_ONE_LIST_SUCCESS,
            payload: data
        })
    } catch (error) {
        dispatch({
            type: GET_CHOUREY_ONE_LIST_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const getChoureyOneDetail = (id, siteId) => async (dispatch) => {
    try {
        dispatch({
            type: CHOUREY_ONE_DETAIL_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        const { data } = await axios.get(
            apiUrl + 'api/Setup/ChoureyOneDetails?choureyOneId=' + id + '&siteId=' + siteId,
            config
        )

        dispatch({
            type: CHOUREY_ONE_DETAIL_SUCCESS,
            payload: data
        })
    } catch (error) {
        dispatch({
            type: CHOUREY_ONE_DETAIL_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const deleteChoureyOne = (params) => async (dispatch) => {
    try {
        dispatch({
            type: CHOUREY_ONE_DELETE_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        const { data } = await axios.delete(
            apiUrl + 'api/Setup/DeleteChoureyOne?choureyId=' + params.id + '&siteId=' + params.siteId,
            config
        )

        dispatch({
            type: CHOUREY_ONE_DELETE_SUCCESS,
            payload: data
        })

        dispatch(getChoureyOneList(params.siteId))
    } catch (error) {
        dispatch({
            type: CHOUREY_ONE_DELETE_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const getChoureyTwoList = (siteId) => async (dispatch) => {
    try {
        dispatch({
            type: GET_CHOUREY_TWO_LIST_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        const { data } = await axios.get(
            apiUrl + 'api/Setup/listChoureyTwo?siteId=' + siteId,
            config
        ) 

        dispatch({
            type: GET_CHOUREY_TWO_LIST_SUCCESS,
            payload: data
        })
    } catch (error) {
        dispatch({
            type: GET_CHOUREY_TWO_LIST_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const getChoureyTwoDetail = (id, siteId) => async (dispatch) => {
    try {
        dispatch({
            type: CHOUREY_TWO_DETAIL_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        const { data } = await axios.get(
            apiUrl + 'api/Setup/ChoureyTwoDetails?choureyTwoId=' + id + '&siteId=' + siteId,
            config
        )

        dispatch({
            type: CHOUREY_TWO_DETAIL_SUCCESS,
            payload: data
        })
    } catch (error) {
        dispatch({
            type: CHOUREY_TWO_DETAIL_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const deleteChoureyTwo = (params) => async (dispatch) => {
    try {
        dispatch({
            type: CHOUREY_TWO_DELETE_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        const { data } = await axios.delete(
            apiUrl + 'api/Setup/DeleteChoureyTwo?choureyId=' 
            + params.id + '&siteId=' + params.siteId,
            config
        )

        dispatch({
            type: CHOUREY_TWO_DELETE_SUCCESS,
            payload: data
        })

        dispatch(getChoureyTwoList(params.siteId))
    } catch (error) {
        dispatch({
            type: CHOUREY_TWO_DELETE_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const createOneChourey = (params) => async (dispatch) => {
    try {
        dispatch({
            type: CREATE_CHOUREY_ONE_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'multipart/form-data',
                Authorization: 'Bearer ' + params.token
            }
        };

        const formData = new FormData();
        formData.append('title', params.title)
        formData.append('SiteId', params.siteId)
        formData.append('GalleyName', "Aasd123@")
        formData.append('isSuccess', true)
        formData.append('Description', params.description)
        formData.append('IsActive', params.isActive)
        formData.append('BrowseRange', params.gpsRange)

        params.files.forEach((element) => {
            let blob = new Blob([element], { type: element.type })
            formData.append('files', blob)
        });

        params.videos.forEach((element) => {
            let blob = new Blob([element], { type: element.type })
            formData.append('videos', blob)
        });

        params.uploads.forEach((element) => {
            let blob = new Blob([element], { type: element.type })
            formData.append('uploads', blob)
        });

        const { data } = await axios.post(
            apiUrl + 'api/Setup/choureyOneInsert',
            formData,
            config
        )

        dispatch({
            type: CREATE_CHOUREY_ONE_SUCCESS,
            payload: data
        })

        dispatch(getChoureyOneList(params.siteId))
    } catch (error) {
        dispatch({
            type: CREATE_CHOUREY_ONE_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const updateChoureyOne = (params) => async (dispatch) => {
    try {
        dispatch({
            type: UPDATE_CHOUREY_ONE_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'multipart/form-data',
                Authorization: 'Bearer ' + params.token
            }
        };

        const formData = new FormData();
        formData.append('ChoureyOneId', params.id)
        formData.append('title', params.title)
        formData.append('SiteId', params.siteId)
        formData.append('GalleyName', "Aasd123@")
        formData.append('galleryId', params.galleryId)
        formData.append('photoId', params.photoId)
        formData.append('isSuccess', true)
        formData.append('Description', params.description)
        formData.append('IsActive', params.isActive)
        formData.append('BrowseRange', params.gpsRange)

        params.files.forEach((element) => {
            let blob = new Blob([element], { type: element.type })
            formData.append('files', blob)
        });

        params.videos.forEach((element) => {
            let blob = new Blob([element], { type: element.type })
            formData.append('videos', blob)
        });

        params.uploads.forEach((element) => {
            let blob = new Blob([element], { type: element.type })
            formData.append('uploads', blob)
        });

        const { data } = await axios.put(
            apiUrl + 'api/Setup/choureyOneEdit',
            formData,
            config
        )

        dispatch({
            type: UPDATE_CHOUREY_ONE_SUCCESS,
            payload: data
        })

        dispatch(getChoureyOneList(params.siteId))
    } catch (error) {
        dispatch({
            type: UPDATE_CHOUREY_ONE_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const createTwoChourey = (params) => async (dispatch) => {
    try {
        dispatch({
            type: CREATE_CHOUREY_TWO_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'multipart/form-data',
                'Access-Control-Allow-Origin': '*',
                Authorization: 'Bearer ' + params.token
            }
        };

        const formData = new FormData();
        formData.append('title', params.title)
        formData.append('SiteId', params.siteId)
        formData.append('GalleyName', "Aasd123@")
        formData.append('isSuccess', true)
        formData.append('Description', params.description)
        formData.append('IsActive', params.isActive)
        formData.append('BrowseRange', params.gpsRange)

        params.files.forEach((element) => {
            let blob = new Blob([element], { type: element.type })
            formData.append('files', blob)
        });

        params.videos.forEach((element) => {
            let blob = new Blob([element], { type: element.type })
            formData.append('videos', blob)
        });

        params.uploads.forEach((element) => {
            let blob = new Blob([element], { type: element.type })
            formData.append('uploads', blob)
        });

        const { data } = await axios.post(
            apiUrl + 'api/Setup/choureyTwoInsert',
            formData,
            config
        )

        dispatch({
            type: CREATE_CHOUREY_TWO_SUCCESS,
            payload: data
        })

        dispatch(getChoureyTwoList(params.siteId))
    } catch (error) {
        dispatch({
            type: CREATE_CHOUREY_TWO_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const updateChoureyTwo = (params) => async (dispatch) => {
    try {
        dispatch({
            type: UPDATE_CHOUREY_TWO_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'multipart/form-data',
                Authorization: 'Bearer ' + params.token
            }
        };

        const formData = new FormData();
        formData.append('ChoureyTwoId', params.id)
        formData.append('title', params.title)
        formData.append('SiteId', params.siteId)
        formData.append('GalleyName', "Aasd123@")
        formData.append('isSuccess', true)
        formData.append('Description', params.description)
        formData.append('IsActive', params.isActive)
        formData.append('BrowseRange', params.gpsRange)

        params.files.forEach((element) => {
            let blob = new Blob([element], { type: element.type })
            formData.append('files', blob)
        });

        params.videos.forEach((element) => {
            let blob = new Blob([element], { type: element.type })
            formData.append('videos', blob)
        });

        params.uploads.forEach((element) => {
            let blob = new Blob([element], { type: element.type })
            formData.append('uploads', blob)
        });

        const { data } = await axios.put(
            apiUrl + 'api/Setup/choureyTwoEdit',
            formData,
            config
        )

        dispatch({
            type: UPDATE_CHOUREY_TWO_SUCCESS,
            payload: data
        })

        dispatch(getChoureyTwoList(params.siteId))
    } catch (error) {
        dispatch({
            type: UPDATE_CHOUREY_TWO_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const deleteMultipleChoureyOne = (params) => async (dispatch) => {
    try {
        dispatch({
            type: MULTIPLE_CHOUREY_ONE_DELETE_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        const { data } = await axios.post(
            apiUrl + 'api/Setup/MultipleDeleteChourey',
            {
                choureyIds: params.id,
                siteId: params.siteId,
                isChoureyOne: true
            },
            config
        )

        dispatch({
            type: MULTIPLE_CHOUREY_ONE_DELETE_SUCCESS,
            payload: data
        })

        dispatch(getChoureyOneList(params.siteId))
    } catch (error) {
        dispatch({
            type: MULTIPLE_CHOUREY_ONE_DELETE_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const deleteMultipleChoureyTwo = (params) => async (dispatch) => {
    try {
        dispatch({
            type: MULTIPLE_CHOUREY_TWO_DELETE_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        const { data } = await axios.post(
            apiUrl + 'api/Setup/MultipleDeleteChourey',
            {
                choureyIds: params.id,
                siteId: params.siteId,
                isChoureyOne: false
            },
            config
        )

        dispatch({
            type: MULTIPLE_CHOUREY_TWO_DELETE_SUCCESS,
            payload: data
        })

        dispatch(getChoureyTwoList(params.siteId))
    } catch (error) {
        dispatch({
            type: MULTIPLE_CHOUREY_TWO_DELETE_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}