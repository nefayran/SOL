import Vuex from "vuex";

import * as user from "./modules/user/index";

export interface RootState {
  user: user.UserState;
}

export default new Vuex.Store<RootState>({
  modules: {
    UserStore: user.store,
  },
});
