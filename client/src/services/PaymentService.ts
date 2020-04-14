import axios from "@/plugins/axios";
import { AppError } from "@/AppError";

export default class UserService {
  public static async addPayment(data) {
    try {
      const response = await axios.post("/Payment/AddPayment", data);
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

  public static async getPayment(id) {
    try {
      const response = await axios.get("/Payment/GetPayment/" + id);
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

  public static async getPayments(options, name) {
    try {
      const response = await axios.post("/Payment/GetPayments?name=" + name, options);
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
}
