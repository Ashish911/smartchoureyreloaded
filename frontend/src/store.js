import { createStore, combineReducers, applyMiddleware } from 'redux'
import thunk from 'redux-thunk'
import { composeWithDevTools } from 'redux-devtools-extension'
import { createUserReducer, forgotPasswordReducer, resetPasswordReducer, userDetailsReducer, userListReducer, userLoginReducer, userRegisterReducer, userUpdatePasswordReducer, userUpdateProfileReducer } from './reducers/userReducers';
import { siteCodeCreateReducer, siteCodeValidateReducer, 
    listSiteReducer, 
    allSiteCodeListReducer,
    siteDetailReducer,
    allSitesSpaceReducer,
    getSiteSpaceReducer,
    createSiteReducer,
    updateSiteReducer,
    getSiteByQrReducer,
    assignSiteSpaceReducer,
    deleteSiteCodeReducer} from './reducers/siteReducers';
import { getMenuListReducer, updateMenuListReducer } from './reducers/menuReducers';
import { createSafetyDeclarationReducer, getSafetyDeclarationListReducer, safetyDeclarationDeleteReducer, updateSafetyDeclarationReducer } from './reducers/safetyReducers';
import { assignSubAdminReducer, getSubAdminListReducer, subAdminDeleteReducer } from './reducers/subAdminReducers';
import { choureyOneDeleteReducer, choureyOneDetailReducer, choureyOneUpdateReducer, choureyTwoDeleteReducer, choureyTwoDetailReducer, choureyTwoUpdateReducer, createChoureyOneReducer, createChoureyTwoReducer, getChoureyOneListReducer, getChoureyTwoListReducer, multipleChoureyOneDeleteReducer, multipleChoureyTwoDeleteReducer } from './reducers/choureyReducers';
import { createDisasterReducer, disasterDeleteReducer, disasterDetailReducer, disasterUpdateReducer, getDisasterListReducer, multipleDisasterDeleteReducer } from './reducers/disasterReducers';
import { getUserBoardReducer } from './reducers/userBoardReducers';
import { getAdminReportListReducer, getDeviceReportListReducer, getLoggedUserReportListReducer, getOperationReportListReducer, getSafetyDeclarationReportListReducer, getSiteUserSmartPhoneReportListReducer, getSmartPhoneUserReportListReducer, getSpSafetyDeclarationReportListReducer, getSubAdminReportListReducer, getUserReportListReducer } from './reducers/reportReducers';
import { assignDeviceMappingReducer, deleteDeviceMappingReducer, getDeviceListReducer, updateDeviceMappingReducer } from './reducers/deviceReducers';
import { deleteFileReducer } from './reducers/fileReducers';
import { addCommentReducer, deleteCommentReducer, getCommentsListReducer } from './reducers/commentReducers';
import { createCommonContentsReducer, deleteCommonContentsReducer, getCommonContentsDetailReducer, getCommonContentsListReducer, updateCommonContentsReducer } from './reducers/commonContentsReducers';

const reducer = combineReducers({
    user: userLoginReducer,
    registerUser: userRegisterReducer,
    userList: userListReducer,
    userCreate: createUserReducer,
    forgotPassword: forgotPasswordReducer,
    resetPassword: resetPasswordReducer,
    site: combineReducers({
        siteCode: siteCodeCreateReducer,
        siteList: allSiteCodeListReducer,
        siteCodeDelete: deleteSiteCodeReducer,
        siteValidation: siteCodeValidateReducer,
        userSiteList: listSiteReducer,
        siteDetail: siteDetailReducer,
        sitesSpace: allSitesSpaceReducer,
        siteSpaceDetail: getSiteSpaceReducer,
        assignSiteSpace: assignSiteSpaceReducer,
        createNewSite: createSiteReducer,
        updateExistingSite: updateSiteReducer,
        qrDetail: getSiteByQrReducer
    }),
    menu: combineReducers({
        menuList: getMenuListReducer,
        menuUpdate: updateMenuListReducer
    }),
    safetyDeclaration: combineReducers({
        safetyList: getSafetyDeclarationListReducer,
        safetyDelete: safetyDeclarationDeleteReducer,
        safetyCreate: createSafetyDeclarationReducer,
        safetyUpdate: updateSafetyDeclarationReducer
    }),
    subAdmin: combineReducers({
        subAdminList: getSubAdminListReducer,
        subAdminAssign: assignSubAdminReducer,
        subAdminDelete: subAdminDeleteReducer
    }),
    chourey: combineReducers({
        choureyOneList: getChoureyOneListReducer,
        choureyOneDetail: choureyOneDetailReducer,
        choureyOneDelete: choureyOneDeleteReducer,
        createChoureyOne: createChoureyOneReducer,
        choureyOneUpdate: choureyOneUpdateReducer,
        choureyTwoList: getChoureyTwoListReducer,
        choureyTwoDetail: choureyTwoDetailReducer,
        choureyTwoDelete: choureyTwoDeleteReducer,
        createChoureyTwo: createChoureyTwoReducer,
        choureyTwoUpdate: choureyTwoUpdateReducer,
        multipleChoureyOneDelete: multipleChoureyOneDeleteReducer,
        multipleChoureyTwoDelete: multipleChoureyTwoDeleteReducer
    }),
    disaster: combineReducers({
        disasterList: getDisasterListReducer,
        disasterDetail: disasterDetailReducer,
        disasterDelete: disasterDeleteReducer,
        createDisaster: createDisasterReducer,
        disasterUpdate: disasterUpdateReducer,
        multipleDisasterDelete: multipleDisasterDeleteReducer
    }),
    profile: combineReducers({
        detail: userDetailsReducer,
        update: userUpdateProfileReducer,
        passwordUpdate: userUpdatePasswordReducer 
    }),
    userBoard: combineReducers({
        board: getUserBoardReducer
    }),
    report: combineReducers({
        userReportList: getUserReportListReducer,
        safetyDeclarationReportList: getSafetyDeclarationReportListReducer,
        smartPhoneUserReportList: getSmartPhoneUserReportListReducer,
        spSafetyDeclarationReportList: getSpSafetyDeclarationReportListReducer,
        siteUserSmartPhoneReportList: getSiteUserSmartPhoneReportListReducer,
        operationReportList: getOperationReportListReducer,
        adminReportList: getAdminReportListReducer,
        subAdminReportList: getSubAdminReportListReducer,
        loggedUserReportList: getLoggedUserReportListReducer,
        deviceReportList: getDeviceReportListReducer
    }),
    device: combineReducers({
        deviceList: getDeviceListReducer,
        deviceAssign: assignDeviceMappingReducer,
        deviceUpdate: updateDeviceMappingReducer,
        deleteDevice: deleteDeviceMappingReducer
    }),
    file: deleteFileReducer,
    comment: combineReducers({
        getComments: getCommentsListReducer,
        deleteComments: deleteCommentReducer,
        addComments: addCommentReducer
    }),
    commonContents: combineReducers({
        listCommonContents: getCommonContentsListReducer,
        commonContentDetail: getCommonContentsDetailReducer,
        createCommonContent: createCommonContentsReducer,
        deleteCommonContent: deleteCommonContentsReducer,
        updateCommonContent: updateCommonContentsReducer
    })
})
let userInfoFromStorage = null

if (localStorage.getItem('userInfo')) {
    if (new Date(JSON.parse(localStorage.getItem('userInfo')).expiryDate * 1000) > new Date()) {
        userInfoFromStorage = JSON.parse(localStorage.getItem('userInfo'))
    } else {
        localStorage.removeItem('userInfo');
    }
}

const initialState = {
    user: { userInfo: userInfoFromStorage}
};

const middleware = [thunk];

const store = createStore(
    reducer,
    initialState,
    composeWithDevTools(applyMiddleware(...middleware))
)

export default store