import axios from "@/plugins/axios";
import { AppError } from "@/AppError";

export default class UserService {
  public static async register(data) {
    try {
      const response = await axios.post("/User/Register", data);
      return response.data;
    } catch (error) {
      if (error.response && error.response.data) {
        throw new AppError(
          error.response.data.error ? error.response.data.error : error.response.data.toString(),
          error.response.data.errorCode || 0
        );
      } else {
        throw error;
      }
    }
  }

  public static async generateResetPasswordCode(data) {
    try {
      const response = await axios.post("/User/GenerateResetPasswordCode", data);
      return response.data;
    } catch (error) {
      if (error.response && error.response.data) {
        throw new AppError(
          error.response.data.error ? error.response.data.error : error.response.data.toString(),
          error.response.data.errorCode || 0
        );
      } else {
        throw error;
      }
    }
  }

  public static async resetPassword(data) {
    try {
      const response = await axios.post("/User/ResetPassword", data);
      return response.data;
    } catch (error) {
      if (error.response && error.response.data) {
        throw new AppError(
          error.response.data.error ? error.response.data.error : error.response.data.toString(),
          error.response.data.errorCode || 0
        );
      } else {
        throw error;
      }
    }
  }

  public static async login(data) {
    try {
      const response = await axios.post("/User/Login", data);
      return response.data;
    } catch (error) {
      if (error.response && error.response.data) {
        throw new AppError(
          error.response.data.error ? error.response.data.error : error.response.data.toString(),
          error.response.data.errorCode || 0
        );
      } else {
        throw error;
      }
    }
  }

  public static async getUsers(options, role, fullName) {
    try {
      const response = await axios.post(`/User/GetUsers?role=${role}&fullName=${fullName}`, options);
      return response.data;
    } catch (error) {
      if (error.response && error.response.data) {
        throw new AppError(
          error.response.data.error ? error.response.data.error : error.response.data.toString(),
          error.response.data.errorCode || 0
        );
      } else {
        throw error;
      }
    }
  }

  public static async loginUser(userName: string) {
    try {
      const response = await axios.post("/User/LoginUser?userName=" + userName);
      return response.data;
    } catch (error) {
      if (error.response && error.response.data) {
        throw new AppError(
          error.response.data.error ? error.response.data.error : error.response.data.toString(),
          error.response.data.errorCode || 0
        );
      } else {
        throw error;
      }
    }
  }

  public static async logout() {
    try {
      const response = await axios.post("/User/Logout");
      return response.data;
    } catch (error) {
      if (error.response && error.response.data) {
        throw new AppError(
          error.response.data.error ? error.response.data.error : error.response.data.toString(),
          error.response.data.errorCode || 0
        );
      } else {
        throw error;
      }
    }
  }

  public static async getUserInfo() {
    try {
      const response = await axios.get("/User/GetUserInfo");
      return response.data;
    } catch (error) {
      if (error.response && error.response.data) {
        throw new AppError(
          error.response.data.error ? error.response.data.error : error.response.data.toString(),
          error.response.data.errorCode || 0
        );
      } else {
        throw error;
      }
    }
  }

  public static async getCaptchaImage() {
    try {
      const response = await axios.get("/Home/CaptchaImage");
      return response.data.data;
    } catch (error) {
      if (error.response && error.response.data) {
        throw new AppError(
          error.response.data.error ? error.response.data.error : error.response.data.toString(),
          error.response.data.errorCode || 0
        );
      } else {
        throw error;
      }
    }
  }

  public static async updateProfilePhoto(avatar) {
    try {
      const response = await axios.post("/User/UpdateAvatar", { avatar });
      return response.data;
    } catch (error) {
      if (error.response && error.response.data) {
        throw new AppError(
          error.response.data.error ? error.response.data.error : error.response.data.toString(),
          error.response.data.errorCode || 0
        );
      } else {
        throw error;
      }
    }
  }

  public static async getUserDashboard() {
    try {
      const response = await axios.get("/User/GetUserDashboard");
      return response.data;
    } catch (error) {
      if (error.response && error.response.data) {
        throw new AppError(
          error.response.data.error ? error.response.data.error : error.response.data.toString(),
          error.response.data.errorCode || 0
        );
      } else {
        throw error;
      }
    }
  }

  public static async updateProfile(data) {
    try {
      const response = await axios.post("/User/UpdateProfile", data);
      return response.data;
    } catch (error) {
      if (error.response && error.response.data) {
        throw new AppError(
          error.response.data.error ? error.response.data.error : error.response.data.toString(),
          error.response.data.errorCode || 0
        );
      } else {
        throw error;
      }
    }
  }

  public static async getProfile(id) {
    try {
      const response = await axios.get(`/User/GetProfile/${id}`);
      return response.data;
    } catch (error) {
      if (error.response && error.response.data) {
        throw new AppError(
          error.response.data.error ? error.response.data.error : error.response.data.toString(),
          error.response.data.errorCode || 0
        );
      } else {
        throw error;
      }
    }
  }

  public static async changePassword(data) {
    try {
      await axios.post("/User/ChangePassword", data);
      return true;
    } catch (error) {
      if (error.response && error.response.data) {
        throw new AppError(
          error.response.data.error ? error.response.data.error : error.response.data.toString(),
          error.response.data.errorCode || 0
        );
      } else {
        throw error;
      }
    }
  }
}
