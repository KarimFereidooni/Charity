export function throttle(func, delay) {
  let nextAllowed = 0;
  return {
    run() {
      const now = Date.now();
      if (now < nextAllowed) {
        return;
      }
      nextAllowed = now + delay;
      func.apply(undefined, arguments);
    },
    reset() {
      nextAllowed = 0;
    }
  };
}

export function debounce(func, delay) {
  let timeoutId: number | undefined;
  return () => {
    clearTimeout(timeoutId);
    const args = arguments;
    timeoutId = setTimeout(() => {
      func.apply(undefined, args);
    }, delay);
  };
}
