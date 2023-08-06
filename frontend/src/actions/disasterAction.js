import axios from 'axios'
import { 
    DISASTER_DETAIL_REQUEST,
    DISASTER_DETAIL_FAIL, 
    DISASTER_DETAIL_SUCCESS, 
    GET_DISASTER_LIST_FAIL,
    GET_DISASTER_LIST_REQUEST, 
    GET_DISASTER_LIST_SUCCESS, 
    DISASTER_DELETE_FAIL,
    DISASTER_DELETE_SUCCESS,
    DISASTER_DELETE_REQUEST,
    CREATE_DISASTER_REQUEST,
    CREATE_DISASTER_SUCCESS,
    CREATE_DISASTER_RESET,
    UPDATE_DISASTER_REQUEST,
    UPDATE_DISASTER_FAIL,
    UPDATE_DISASTER_SUCCESS,
    MULTIPLE_DISASTER_DELETE_FAIL,
    MULTIPLE_DISASTER_DELETE_SUCCESS,
    MULTIPLE_DISASTER_DELETE_REQUEST
} from '../contsants/disasterConstants';

const apiUrl = process.env.REACT_APP_BASE_URL;

export const getDisasterList = (siteId) => async (dispatch) => {
    try {
        dispatch({
            type: GET_DISASTER_LIST_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        const { data } = await axios.get(
            apiUrl + 'api/Setup/listDisastersBySiteId?siteId=' + siteId,
            config
        ) 

        dispatch({
            type: GET_DISASTER_LIST_SUCCESS,
            payload: data
        })
    } catch (error) {
        dispatch({
            type: GET_DISASTER_LIST_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const getDisasterDetail = (id, siteId) => async (dispatch) => {
    try {
        dispatch({
            type: DISASTER_DETAIL_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        const { data } = await axios.get(
            apiUrl + 'api/Setup/GetDisasterDetailsById?disasterId=' + id + '&siteId=' + siteId,
            config
        )

        dispatch({
            type: DISASTER_DETAIL_SUCCESS,
            payload: data
        })
    } catch (error) {
        dispatch({
            type: DISASTER_DETAIL_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const deleteDisaster = (id, siteId) => async (dispatch) => {
    try {
        dispatch({
            type: DISASTER_DELETE_REQUEST
        });
    
        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };
    
        await axios.delete(`${apiUrl}api/siteCode/${id}`, config);
    
        dispatch({
            type: DISASTER_DELETE_SUCCESS
        });
    } catch (error) {
        dispatch({
            type: DISASTER_DELETE_FAIL,
            payload:
            error.response && error.response.data.message
                ? error.response.data.message
                : error.message
        });
    }
};

export const disasterCreate = (params) => async (dispatch) => {
    try {
        dispatch({
            type: CREATE_DISASTER_REQUEST
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
        formData.append('VideoFilePath', "Aasd123@")
        formData.append('isSuccess', true)
        formData.append('Description', params.description)
        formData.append('IsActive', params.isActive)
        formData.append('BrowseRange', params.gpsRange)

        params.files.forEach((element) => {
            let blob = new Blob([element], { type: element.type })
            formData.append('files', blob)
        });

        params.uploads.forEach((element) => {
            let blob = new Blob([element], { type: element.type })
            formData.append('uploads', blob)
        });

        const { data } = await axios.post(
            apiUrl + 'api/Setup/DisasterInsert',
            formData,
            config
        )

        dispatch({
            type: CREATE_DISASTER_SUCCESS,
            payload: data
        })

        dispatch(getDisasterList(params.siteId))
    } catch (error) {
        dispatch({
            type: CREATE_DISASTER_RESET,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const updateDisaster = (params) => async (dispatch) => {
    try {
        dispatch({
            type: UPDATE_DISASTER_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'multipart/form-data',
                Authorization: 'Bearer ' + params.token
            }
        };

        const formData = new FormData();
        formData.append('DisasterId', params.id)
        formData.append('title', params.title)
        formData.append('SiteId', params.siteId)
        formData.append('GalleyName', "Aasd123@")
        formData.append('VideoFilePath', "Aasd123@")
        formData.append('isSuccess', true)
        formData.append('Description', params.description)
        formData.append('IsActive', params.isActive)
        formData.append('BrowseRange', params.gpsRange)

        params.files.forEach((element) => {
            let blob = new Blob([element], { type: element.type })
            formData.append('files', blob)
        });

        params.uploads.forEach((element) => {
            let blob = new Blob([element], { type: element.type })
            formData.append('uploads', blob)
        });

        const { data } = await axios.put(
            apiUrl + 'api/Setup/DisasterEdit',
            formData,
            config
        )

        dispatch({
            type: UPDATE_DISASTER_SUCCESS,
            payload: data
        })

        dispatch(getDisasterList(params.siteId))
    } catch (error) {
        dispatch({
            type: UPDATE_DISASTER_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const deleteMultipleDisaster = (params) => async (dispatch) => {
    try {
        dispatch({
            type: MULTIPLE_DISASTER_DELETE_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
                Authorization: 'Bearer ' + params.token
            }
        };

        const { data } = await axios.post(
            apiUrl + 'api/Setup/MultipleDeleteDisaster?disasterIds',
            {
                disasterIds: params.id,
                siteId: params.siteId
            },
            config
        )

        dispatch({
            type: MULTIPLE_DISASTER_DELETE_SUCCESS,
            payload: data
        })

        dispatch(getDisasterList(params.siteId))
    } catch (error) {
        dispatch({
            type: MULTIPLE_DISASTER_DELETE_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}