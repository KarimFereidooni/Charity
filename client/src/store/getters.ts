import { AppState } from ".";

export const getters = {
  userIsAdmin: (state: AppState) => {
    return (
      state.userInfo.isAuthenticated &&
      state.userInfo.roles.some(currentValue => {
        return currentValue.name === "Admin";
      })
    );
  },
  userIsCharity: (state: AppState) => {
    return (
      state.userInfo.isAuthenticated &&
      state.userInfo.roles.some(currentValue => {
        return currentValue.name === "Charity";
      })
    );
  },
  userIsUser: (state: AppState) => {
    return (
      state.userInfo.isAuthenticated &&
      state.userInfo.roles.some(currentValue => {
        return currentValue.name === "User";
      })
    );
  }
};
