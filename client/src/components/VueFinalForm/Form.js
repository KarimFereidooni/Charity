import { createForm, formSubscriptionItems } from "final-form";
import assign from "nano-assign";
import { getChildren, composeFormValidators } from "./utils";

const defaultSubscription = formSubscriptionItems.reduce((result, key) => {
  result[key] = true;
  return result;
}, {});

export default {
  name: "final-form",

  props: {
    initialValues: Object,
    submit: {
      type: Function,
      default: () => {
        /**/
      }
    },
    subscription: Object,
    validate: [Function, Array],
    validateOnBlur: {
      type: Boolean,
      default: false
    }
  },

  provide() {
    return {
      finalForm: this.finalForm
    };
  },

  data() {
    return {
      finalForm: createForm({
        onSubmit: this.submit,
        initialValues: this.initialValues,
        validateOnBlur: this.validateOnBlur,
        validate: Array.isArray(this.validate)
          ? composeFormValidators(this.validate)
          : this.validate
      }),
      formState: null
    };
  },

  methods: {
    handleSubmit(e) {
      if (e) {
        e.preventDefault();
      }
      for (const p in this.formState.errors) {
        if (this.formState.errors[p]) {
          document.querySelectorAll(`[name="${p}"]:not(meta)`)[0].focus();
          this.finalForm.submit();
          return;
        }
      }
      this.finalForm.submit();
    }
  },

  created() {
    this.unsubscribe = this.finalForm.subscribe(state => {
      this.formState = state;
      this.$emit("change", { state: state, formApi: this.finalForm });
    }, this.subscription || defaultSubscription);
  },

  beforeDestroy() {
    this.unsubscribe();
  },

  render(h) {
    const children = this.$scopedSlots.default
      ? this.$scopedSlots.default(
          assign({}, this.formState, {
            handleSubmit: this.handleSubmit,
            mutators: this.finalForm.mutators,
            batch: this.finalForm.batch,
            blur: this.finalForm.blur,
            change: this.finalForm.change,
            focus: this.finalForm.focus,
            initialize: this.finalForm.initialize,
            reset: this.finalForm.reset
          })
        )
      : this.$slots.default;

    return h("div", null, getChildren(children));
  }
};
