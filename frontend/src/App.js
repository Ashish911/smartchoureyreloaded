import './App.css';
import LoginPage from './screens/LoginPage';
import RegisterPage from './screens/RegisterPage';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom'
import { ToastContainer } from "react-toastify";
import MainDashboardPage from './screens/MainDashboardPage';
import i18next from 'i18next'
import { useEffect, useState } from 'react';
import ForgotPassword from './screens/ForgotPassword';
import SuperAdminMainDashboardPage from './screens/SuperAdminMainDashboardPage';
import UserBoardDashboardPage from './screens/UserBoardDashboardPage';
import ResetPassword from './screens/ResetPassword';

function App() {

  const onchange = (e) => {
    localStorage.setItem('currentLanguage', e.target.value)
    i18next.changeLanguage(e.target.value)
  }

  useEffect(() => {
    console.log(process.env.REACT_APP_BASE_URL)
    if (localStorage.getItem('currentLanguage')) {
      i18next.changeLanguage(localStorage.getItem('currentLanguage'))
    }
  }, []);

  return (
    <Router>
      <div className="App">
        <ToastContainer />
        <Routes>
          {/* This is login routes */}
          <Route path="/" element={<LoginPage onchange={onchange} i18next={i18next} />}/>
          <Route path="/register" element={<RegisterPage onchange={onchange} i18next={i18next}/>}/>
          <Route path="/forgotPassword" element={<ForgotPassword onchange={onchange} i18next={i18next}/>}/>
          <Route path="/resetPassword" element={<ResetPassword onchange={onchange} i18next={i18next}/>}/>
          
          {/* This is Super Admin Routes */}
          <Route path='/adminDashboard' element={<SuperAdminMainDashboardPage onchange={onchange} i18next={i18next}/>}/>
          <Route path='/adminDashboard/mapping' element={<SuperAdminMainDashboardPage selectedLink='adminDashboard/mapping' onchange={onchange} i18next={i18next}/>}/>
          <Route path='/adminDashboard/siteStorage' element={<SuperAdminMainDashboardPage selectedLink='adminDashboard/siteStorage' onchange={onchange} i18next={i18next}/>}/>
          <Route path='/adminDashboard/commonContents' element={<SuperAdminMainDashboardPage selectedLink='adminDashboard/commonContents' onchange={onchange} i18next={i18next}/>}/>
          <Route path='/adminDashboard/createCommonContents' element={<SuperAdminMainDashboardPage selectedLink='adminDashboard/createCommonContents' onchange={onchange} i18next={i18next}/>}/>
          <Route path='/adminDashboard/commonContentDetails' element={<SuperAdminMainDashboardPage selectedLink='adminDashboard/commonContentDetails' onchange={onchange} i18next={i18next}/>} />
          <Route path='/adminDashboard/report' element={<SuperAdminMainDashboardPage selectedLink='adminDashboard/report' onchange={onchange} i18next={i18next}/>}/>
          <Route path='/adminDashboard/userList' element={<SuperAdminMainDashboardPage selectedLink='adminDashboard/userList' onchange={onchange} i18next={i18next}/>}/>

          {/* This is Admin/Sub Admin Routes */}
          <Route path='/dashboard' element={<MainDashboardPage selectedLink='dashboard' onchange={onchange} i18next={i18next}/>}/>
          <Route path='/dashboard/userboard' element={<MainDashboardPage selectedLink='dashboard/userboard' onchange={onchange} i18next={i18next}/>}/>
          <Route path='/dashboard/menu' element={<MainDashboardPage selectedLink='dashboard/menu' onchange={onchange} i18next={i18next}/>}/>
          <Route path='/dashboard/choureyOne' element={<MainDashboardPage selectedLink='dashboard/choureyOne' onchange={onchange} i18next={i18next}/>}/>
          <Route path='/dashboard/choureyTwo' element={<MainDashboardPage selectedLink='dashboard/choureyTwo' onchange={onchange} i18next={i18next}/>}/>
          <Route path='/dashboard/disaster' element={<MainDashboardPage selectedLink='dashboard/disaster' onchange={onchange} i18next={i18next}/>}/>
          <Route path='/dashboard/safetyDeclaration' element={<MainDashboardPage selectedLink='dashboard/safetyDeclaration' onchange={onchange} i18next={i18next}/>}/>
          <Route path='/dashboard/report' element={<MainDashboardPage selectedLink='dashboard/report' onchange={onchange} i18next={i18next}/>}/>
          <Route path='/dashboard/print' element={<MainDashboardPage selectedLink='dashboard/print' onchange={onchange} i18next={i18next}/>}/>
          <Route path='/dashboard/subAdmin' element={<MainDashboardPage selectedLink='dashboard/subAdmin' onchange={onchange} i18next={i18next}/>}/>
          <Route path='/dashboard/profile' element={<MainDashboardPage selectedLink='dashboard/profile' onchange={onchange} i18next={i18next}/>}/>
          <Route path='/dashboard/changePassword' element={<MainDashboardPage selectedLink='dashboard/changePassword' onchange={onchange} i18next={i18next}/>}/>
          <Route path='/dashboard/createSite' element={<MainDashboardPage selectedLink='dashboard/createSite' onchange={onchange} i18next={i18next} />} />
          <Route path='/dashboard/siteDetail' element={<MainDashboardPage selectedLink='dashboard/siteDetail' onchange={onchange} i18next={i18next} />} />
          <Route path='/dashboard/createDisaster' element={<MainDashboardPage selectedLink='dashboard/createDisaster' onchange={onchange} i18next={i18next}/>} />
          <Route path='/dashboard/editDisaster' element={<MainDashboardPage selectedLink='dashboard/editDisaster' onchange={onchange} i18next={i18next}/>} />
          <Route path='/dashboard/disasterDetail' element={<MainDashboardPage selectedLink='dashboard/disasterDetail' onchange={onchange} i18next={i18next}/>} />
          <Route path='/dashboard/createChoureyOne' element={<MainDashboardPage selectedLink='dashboard/createChoureyOne' onchange={onchange} i18next={i18next}/>} />
          <Route path='/dashboard/createChoureyOneEdit' element={<MainDashboardPage selectedLink='dashboard/createChoureyOneEdit' onchange={onchange} i18next={i18next}/>} />
          <Route path='/dashboard/choureyOneDetail' element={<MainDashboardPage selectedLink='dashboard/choureyOneDetail' onchange={onchange} i18next={i18next}/>} />
          <Route path='/dashboard/createChoureyTwo' element={<MainDashboardPage selectedLink='dashboard/createChoureyTwo' onchange={onchange} i18next={i18next}/>} />
          <Route path='/dashboard/createChoureyTwoEdit' element={<MainDashboardPage selectedLink='dashboard/createChoureyTwoEdit' onchange={onchange} i18next={i18next}/>} />
          <Route path='/dashboard/choureyTwoDetail' element={<MainDashboardPage selectedLink='dashboard/choureyTwoDetail' onchange={onchange} i18next={i18next}/>} />

          {/* This is User Routes */}
          <Route path='/userDashboard' element={<UserBoardDashboardPage onchange={onchange} i18next={i18next}/>}/>
          <Route path='/userDashboard/createSite' element={<UserBoardDashboardPage selectedLink='userDashboard/createSite' onchange={onchange} i18next={i18next}/>}/>
          <Route path='/userDashboard/siteDetail' element={<UserBoardDashboardPage selectedLink='userDashboard/siteDetail' onchange={onchange} i18next={i18next}/>}/>
          <Route path='/userDashboard/disasterDetail' element={<UserBoardDashboardPage selectedLink='userDashboard/disasterDetail' onchange={onchange} i18next={i18next}/>} />
          <Route path='/userDashboard/choureyOneDetail' element={<UserBoardDashboardPage selectedLink='userDashboard/choureyOneDetail' onchange={onchange} i18next={i18next}/>} />
          <Route path='/userDashboard/choureyTwoDetail' element={<UserBoardDashboardPage selectedLink='userDashboard/choureyTwoDetail' onchange={onchange} i18next={i18next}/>} />

        </Routes>

      </div>
    </Router>
  );
}

export default App;
