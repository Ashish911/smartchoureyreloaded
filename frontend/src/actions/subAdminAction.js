import {
    SUB_ADMIN_LIST_REQUEST,
    SUB_ADMIN_LIST_SUCCESS,
    SUB_ADMIN_LIST_FAIL,
    SUB_ADMIN_LIST_RESET,
    SUB_ADMIN_ASSIGN_REQUEST,
    SUB_ADMIN_ASSIGN_SUCCESS,
    SUB_ADMIN_ASSIGN_FAIL,
    SUB_ADMIN_ASSIGN_RESET,
    SUB_ADMIN_DELETE_SUCCESS,
    SUB_ADMIN_DELETE_FAIL,
    SUB_ADMIN_DELETE_REQUEST
} from '../contsants/subAdminConstants'
import axios from 'axios'

const apiUrl = process.env.REACT_APP_BASE_URL;

export const getSubAdminList = (siteId) => async (dispatch) => {
    try {
        dispatch({
            type: SUB_ADMIN_LIST_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        const { data } = await axios.get(
            apiUrl + 'api/Subadmin/GetSubAdminsBySiteId?siteId=' + siteId,
            config
        ) 

        dispatch({
            type: SUB_ADMIN_LIST_SUCCESS,
            payload: data
        })
    } catch (error) {
        dispatch({
            type: SUB_ADMIN_LIST_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const assignSubAdmin = (siteId, email, token) => async (dispatch) => {
    try {
        dispatch({
            type: SUB_ADMIN_ASSIGN_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
                Authorization: 'Bearer ' + token
            }
        };

        const { data } = await axios.post(
            apiUrl + 'api/Subadmin/CreateSubAdmin?email='+email+'&siteId='+siteId,
            { 
                id: 'None',
            },
            config
        ) 

        dispatch({
            type: SUB_ADMIN_ASSIGN_SUCCESS,
            payload: data
        })
        dispatch(getSubAdminList(siteId))
    } catch (error) {
        dispatch({
            type: SUB_ADMIN_ASSIGN_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const deleteSubAdmin = (params) => async (dispatch) => {
    try {
        dispatch({
            type: SUB_ADMIN_DELETE_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
                Authorization: 'Bearer ' + params.token
            }
        };

        const { data } = await axios.delete(
            apiUrl + 'api/Subadmin/DeleteSiteSubAdmin?id=' + params.id ,
            config
        )

        dispatch({
            type: SUB_ADMIN_DELETE_SUCCESS,
            payload: data
        })

        dispatch(getSubAdminList(params.siteId))
    } catch (error) {
        dispatch({
            type: SUB_ADMIN_DELETE_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}