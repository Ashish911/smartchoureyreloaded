import axios from 'axios'
import { SAFETY_DECLARATION_DELETE_FAIL, SAFETY_DECLARATION_DELETE_REQUEST, SAFETY_DECLARATION_DELETE_SUCCESS, SAFETY_DECLARATION_LIST_FAIL, SAFETY_DECLARATION_LIST_REQUEST, SAFETY_DECLARATION_LIST_SUCCESS, UPDATE_SAFETY_DECLARATION_FAIL, UPDATE_SAFETY_DECLARATION_REQUEST, UPDATE_SAFETY_DECLARATION_SUCCESS } from '../contsants/safetyDeclarationConstants';
import { CREATE_SAFETY_DECLARATION_REQUEST } from '../contsants/safetyDeclarationConstants';
import { CREATE_SAFETY_DECLARATION_SUCCESS } from '../contsants/safetyDeclarationConstants';
import { CREATE_SAFETY_DECLARATION_FAIL } from '../contsants/safetyDeclarationConstants';

const apiUrl = process.env.REACT_APP_BASE_URL;

export const getSafetyDeclarationList = (siteId) => async (dispatch) => {
    try {
        dispatch({
            type: SAFETY_DECLARATION_LIST_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        const { data } = await axios.get(
            apiUrl + 'api/SafetyDeclaration/siteDeclarationBySite?siteId=' + siteId + '&viewMode=1',
            config
        )

        dispatch({
            type: SAFETY_DECLARATION_LIST_SUCCESS,
            payload: data
        })
    } catch (error) {
        dispatch({
            type: SAFETY_DECLARATION_LIST_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const createSafetyDeclaration = (params) => async (dispatch) => {
    try {
        dispatch({
            type: CREATE_SAFETY_DECLARATION_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        const { data } = await axios.post(
            apiUrl + 'api/SafetyDeclaration/insertUpdateSiteDeclaration',
            { 
                siteDeclarationId : 0,
                description: params.description,
                ViewMode: 1,
                title: params.title,
                isActive: params.isActive,
                siteId: params.siteId,
                siteName: params.siteName,
                currentUserId: params.currentUserId,
                descriptionModel: {
                    description: params.description
                }
            },
            config
        )

        dispatch({
            type: CREATE_SAFETY_DECLARATION_SUCCESS,
            payload: data
        })

        dispatch(getSafetyDeclarationList(params.siteId))
    } catch (error) {
        dispatch({
            type: CREATE_SAFETY_DECLARATION_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const updateSafetyDeclaration = (params) => async (dispatch) => {
    try {
        dispatch({
            type: UPDATE_SAFETY_DECLARATION_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        const { data } = await axios.post(
            apiUrl + 'api/SafetyDeclaration/insertUpdateSiteDeclaration',
            { 
                siteDeclarationId : params.id,
                description: params.description,
                ViewMode: 1,
                title: params.title,
                isActive: params.isActive,
                siteId: params.siteId,
                siteName: params.siteName,
                currentUserId: params.currentUserId,
                descriptionModel: {
                    description: params.description
                }
            },
            config
        )

        dispatch({
            type: UPDATE_SAFETY_DECLARATION_SUCCESS,
            payload: data
        })

        dispatch(getSafetyDeclarationList(params.siteId))
    } catch (error) {
        dispatch({
            type: UPDATE_SAFETY_DECLARATION_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const deleteSafetyDeclaration = (params) => async (dispatch) => {
    try {
        dispatch({
            type: SAFETY_DECLARATION_DELETE_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        const { data } = await axios.delete(
            apiUrl + 'api/SafetyDeclaration/deleteSiteDeclaration?siteDeclarationId=' 
            + params.id + '&siteId=' + params.siteId,
            config
        )

        dispatch({
            type: SAFETY_DECLARATION_DELETE_SUCCESS,
            payload: data
        })

        dispatch(getSafetyDeclarationList(params.siteId))
    } catch (error) {
        dispatch({
            type: SAFETY_DECLARATION_DELETE_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}