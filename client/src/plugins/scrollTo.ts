export const easing = {
  // no easing, no acceleration
  linear(t) {
    return t;
  },
  // accelerating from zero velocity
  easeInQuad(t) {
    return t * t;
  },
  // decelerating to zero velocity
  easeOutQuad(t) {
    return t * (2 - t);
  },
  // acceleration until halfway, then deceleration
  easeInOutQuad(t) {
    return t < 0.5 ? 2 * t * t : -1 + (4 - 2 * t) * t;
  },
  // accelerating from zero velocity
  easeInCubic(t) {
    return t * t * t;
  },
  // decelerating to zero velocity
  easeOutCubic(t) {
    return --t * t * t + 1;
  },
  // acceleration until halfway, then deceleration
  easeInOutCubic(t) {
    return t < 0.5 ? 4 * t * t * t : (t - 1) * (2 * t - 2) * (2 * t - 2) + 1;
  },
  // accelerating from zero velocity
  easeInQuart(t) {
    return t * t * t * t;
  },
  // decelerating to zero velocity
  easeOutQuart(t) {
    return 1 - --t * t * t * t;
  },
  // acceleration until halfway, then deceleration
  easeInOutQuart(t) {
    return t < 0.5 ? 8 * t * t * t * t : 1 - 8 * --t * t * t * t;
  },
  // accelerating from zero velocity
  easeInQuint(t) {
    return t * t * t * t * t;
  },
  // decelerating to zero velocity
  easeOutQuint(t) {
    return 1 + --t * t * t * t * t;
  },
  // acceleration until halfway, then deceleration
  easeInOutQuint(t) {
    return t < 0.5 ? 16 * t * t * t * t * t : 1 + 16 * --t * t * t * t * t;
  }
};

export function scrollTo(
  Y,
  duration,
  easingFunction = easing.linear,
  callback?,
  element?
) {
  // tslint:disable-next-line: one-variable-per-declaration
  const start = Date.now(),
    elem = element
      ? element
      : document.documentElement.scrollTop
      ? document.documentElement
      : document.body,
    from = elem.scrollTop;

  if (from === Y) {
    if (callback) {
      callback();
    }
    return; /* Prevent scrolling to the Y point if already there */
  }

  function min(a, b) {
    return a < b ? a : b;
  }

  function scroll() {
    // tslint:disable-next-line: one-variable-per-declaration
    const currentTime = Date.now(),
      time = min(1, (currentTime - start) / duration),
      easedT = easingFunction(time);

    elem.scrollTop = easedT * (Y - from) + from;

    if (time < 1) {
      requestAnimationFrame(scroll);
    } else {
      if (callback) {
        callback();
      }
    }
  }
  requestAnimationFrame(scroll);
}
