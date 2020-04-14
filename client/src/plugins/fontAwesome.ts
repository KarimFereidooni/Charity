import Vue from "vue";
import { library } from "@fortawesome/fontawesome-svg-core";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import { fas } from "@fortawesome/free-solid-svg-icons";
import { faSquare } from "@fortawesome/free-regular-svg-icons/faSquare";
import { faTwitter } from "@fortawesome/free-brands-svg-icons/faTwitter";
import { faFacebook } from "@fortawesome/free-brands-svg-icons/faFacebook";
import { faLinkedin } from "@fortawesome/free-brands-svg-icons/faLinkedin";
import { faGooglePlus } from "@fortawesome/free-brands-svg-icons/faGooglePlus";
import { faInstagram } from "@fortawesome/free-brands-svg-icons/faInstagram";
import { faStar } from "@fortawesome/free-regular-svg-icons/faStar";

export const icons = {
  facebook: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fab", "facebook"]
    }
  },
  twitter: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fab", "twitter"]
    }
  },
  googlePlus: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fab", "google-plus"]
    }
  },
  instagram: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fab", "instagram"]
    }
  },
  linkedin: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fab", "linkedin"]
    }
  },
  square: {
    component: FontAwesomeIcon,
    props: {
      icon: ["far", "square"]
    }
  },
  menu: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fas", "bars"]
    }
  },
  home: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fas", "home"]
    }
  },
  chevronUp: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fas", "chevron-up"]
    }
  },
  chevronLeft: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fas", "chevron-left"]
    }
  },
  chevronRight: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fas", "chevron-right"]
    }
  },
  user: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fas", "user"]
    }
  },
  userCircle: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fas", "user-circle"]
    }
  },
  userTie: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fas", "user-tie"]
    }
  },
  key: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fas", "key"]
    }
  },
  mobileAlt: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fas", "mobile-alt"]
    }
  },
  squareRootAlt: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fas", "square-root-alt"]
    }
  },
  login: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fas", "sign-in-alt"]
    }
  },
  logout: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fas", "sign-out-alt"]
    }
  },
  register: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fas", "user-plus"]
    }
  },
  file: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fas", "file"]
    }
  },
  language: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fas", "language"]
    }
  },
  dollar: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fas", "dollar-sign"]
    }
  },
  newspaper: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fas", "newspaper"]
    }
  },
  clock: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fas", "clock"]
    }
  },
  tachometerAlt: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fas", "tachometer-alt"]
    }
  },
  columns: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fas", "columns"]
    }
  },
  globeAmericas: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fas", "globe-americas"]
    }
  },
  infoCircle: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fas", "info-circle"]
    }
  },
  exclamationTriangle: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fas", "exclamation-triangle"]
    }
  },
  folder: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fas", "folder"]
    }
  },
  delete: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fas", "trash-alt"]
    }
  },
  edit: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fas", "pencil-alt"]
    }
  },
  download: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fas", "download"]
    }
  },
  search: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fas", "search"]
    }
  },
  calendarDay: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fas", "calendar-day"]
    }
  },
  expand: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fas", "chevron-down"]
    }
  },
  dropdown: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fas", "caret-down"]
    }
  },
  next: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fas", "chevron-right"]
    }
  },
  prev: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fas", "chevron-left"]
    }
  },
  sort: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fas", "caret-down"]
    }
  },
  warning: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fas", "exclamation-triangle"]
    }
  },
  error: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fas", "exclamation-triangle"]
    }
  },
  checkboxOff: {
    component: FontAwesomeIcon,
    props: {
      icon: ["far", "square"]
    }
  },
  checkboxOn: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fas", "check-square"]
    }
  },
  ratingEmpty: {
    component: FontAwesomeIcon,
    props: {
      icon: ["far", "star"]
    }
  },
  ratingFull: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fas", "star"]
    }
  },
  ratingHalf: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fas", "star-half-alt"]
    }
  },
  tick: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fas", "check"]
    }
  },
  clear: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fas", "times"]
    }
  },
  cancel: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fas", "times"]
    }
  },
  info: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fas", "info"]
    }
  },
  handshake: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fas", "handshake"]
    }
  },
  wallet: {
    component: FontAwesomeIcon,
    props: {
      icon: ["fas", "wallet"]
    }
  }
};

Vue.component("font-awesome-icon", FontAwesomeIcon);
library.add(
  fas,
  faSquare,
  faFacebook,
  faTwitter,
  faGooglePlus,
  faInstagram,
  faLinkedin,
  faStar
);
