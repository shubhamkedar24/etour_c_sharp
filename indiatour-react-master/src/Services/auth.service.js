import axios from "axios";
import { useNavigate } from "react-router-dom"


const API_URL = "https://localhost:44337/api/auth/";


class AuthService {
  login(email, password) {
    return axios
      .post(API_URL + "login", {
        // we did this not the old one
        // email,
        // password
        email : email,
        password : password
      })
      .then(response => {
        if (response.data.accessToken) {
          // if (response.data.obj) {
          localStorage.setItem("user", JSON.stringify(response.data));
          
          // alert(user.cust_Id);
          // alert(user.password);
          // alert(user.accessToken);
          // alert(user.roles);
        }
        return response.data;
      })
      // code added by us catch block
      // .catch(error=>{alert("user not present")})
      ;
  }

  logout() {
    localStorage.removeItem("user");
  }

  register(firstname, lastname, email, mobile, dob, gender, password) {
    return axios
    .post(API_URL + "signup", {
      firstname,
      lastname,
      email,
      mobile,
      dob,
      gender,
      password
    // });
    // we wrote this code
    }).then(response => {
      window.location.replace(`http://localhost:3000/login`);
    })
  }

  getCurrentUser() {
    return JSON.parse(localStorage.getItem('user'));;
  }
}

export default new AuthService();
