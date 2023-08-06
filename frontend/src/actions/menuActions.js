import { 
    MENU_LIST_FAIL, 
    MENU_LIST_REQUEST, 
    MENU_LIST_SUCCESS, 
    UPDATE_MENU_LIST_FAIL, 
    UPDATE_MENU_LIST_REQUEST, 
    UPDATE_MENU_LIST_SUCCESS 
} from "../contsants/menuConstants";
import axios from 'axios'

const apiUrl = process.env.REACT_APP_BASE_URL;

export const getMenuList = (siteCode) => async (dispatch) => {
    try {
        dispatch({
            type: MENU_LIST_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        const { data } = await axios.get(
            apiUrl + 'api/ChoureyCustomName/getCustomChoureyNameBySiteId?siteId=' + siteCode,
            config
        )

        dispatch({
            type: MENU_LIST_SUCCESS,
            payload: data
        })
    } catch (error) {
        dispatch({
            type: MENU_LIST_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const updateMenuList = (params) => async (dispatch) => {
    try {
        dispatch({
            type: UPDATE_MENU_LIST_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        const { data } = await axios.post(
            apiUrl + 'api/ChoureyCustomName/ChoureyCustomNameCreate',
            {
                choureyCustomNameId: params.choureyCustomNameId,
                chourey1: params.chourey1,
                chourey2: params.chourey2,
                disaster: params.disaster,
                saftetyDeclaration: params.saftetyDeclaration,
                siteId: params.siteId,
                chourey1Japanese: params.chourey1Japanese,
                chourey2Japanese: params.chourey2Japanese,
                disasterJapanese: params.disasterJapanese,
                saftetyDeclarationJapanese: params.saftetyDeclarationJapanese
            },
            config
        )

        dispatch({
            type: UPDATE_MENU_LIST_SUCCESS
        })

        dispatch(getMenuList(params.siteId))
    } catch (error) {
        dispatch({
            type: UPDATE_MENU_LIST_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}