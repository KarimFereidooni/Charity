import axios from "@/plugins/axios";
import { AppError } from "@/AppError";

export default class CharityService {
  public static async getCharities(data) {
    try {
      const response = await axios.post("/Charity/GetCharities", data);
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
