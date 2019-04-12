  namespace Sweep

  module Exceptions =
    exception NotFoundException of string
    exception RenderException of string